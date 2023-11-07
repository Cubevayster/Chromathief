using System;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class AIBrain : MonoBehaviour
{
    public Action<ALERT_TYPES> OnAlertTypeChanged = null;

    [SerializeField] ALERT_TYPES alertType = ALERT_TYPES.None;
    [SerializeField] AIVision vision = null;
    [SerializeField] AIMovement movement = null;
    [SerializeField] AIPathFollower pathfollow = null;
    [SerializeField] SoundReceiver soundReceiver = null;
    [SerializeField] SpotcamComponent spotcamComponent = null;

    [SerializeField] bool isFollowingPath = true;

    Coroutine currentWaitCoroutine = null;

    public ALERT_TYPES GetAlertType() => alertType;

    private void Awake() => RegisterCallbacks();

    private void Start() => EntityManager.Instance?.RegisterGuard(this);
    private void OnDestroy() => EntityManager.Instance?.UnregisterGuard(this);

    void RegisterCallbacks()
    {
        if(movement)
        {
            OnAlertTypeChanged += movement.ChangeMaxSpeed;
        }

        if(pathfollow && movement)
        {
            pathfollow.OnPathDestinationAcquired += movement.MoveToDestination;
            movement.OnDestinationReached += IALogicOnDestinationReached;
            movement.OnEndWait += pathfollow.GetNextPosition;
        }

        if(vision)
        {
            vision.OnPlayerSpotted += IALogicOnSpotPlayer;
        }

        if(vision && spotcamComponent)
        {
            vision.OnPlayerStillInSight += spotcamComponent.FollowPlayer;
        }

        if(soundReceiver)
        {
            soundReceiver.OnSoundReceived += IALogicOnSoundHeard;
        }
    }

    void IALogicOnDestinationReached()
    {
        switch (alertType)
        {
            case ALERT_TYPES.None:
                if (!pathfollow) return;
                currentWaitCoroutine = StartCoroutine(movement.StopMovement(pathfollow.GetTimeToWaitAtEachPoint()));
            break;

            case ALERT_TYPES.Warning:
                currentWaitCoroutine = StartCoroutine(movement.StopMovement(2));
                alertType = ALERT_TYPES.None;
                OnAlertTypeChanged.Invoke(alertType);
                break;
        }
    }

    void IALogicOnSpotPlayer(Player _player)
    {
        if(currentWaitCoroutine != null) StopCoroutine(currentWaitCoroutine);
        if (movement) movement.StopMovement();

        PlayerControler _playerControler = _player.GetPlayerControler();
        if(_playerControler) _playerControler.StopPlayerControler();
        
        StartCoroutine(GameManager.GameOver(spotcamComponent));
    }

    void IALogicOnSoundHeard(Vector3 _positionSound, float _rangeSound)
    {
        float _distanceToSound = (_positionSound-transform.position).magnitude;
        if (_rangeSound != -1 && _rangeSound < _distanceToSound) return;
        alertType = ALERT_TYPES.Warning;
        OnAlertTypeChanged.Invoke(alertType);
        if (currentWaitCoroutine != null) StopCoroutine(currentWaitCoroutine);
        movement.MoveToDestination(_positionSound);
    }
}
