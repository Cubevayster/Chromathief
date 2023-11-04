using UnityEngine;

public class AIBrain : MonoBehaviour
{
    [SerializeField] AIVision vision = null;
    [SerializeField] AIMovement movement = null;
    [SerializeField] AIPathFollower pathfollow = null;

    [SerializeField] bool isFollowingPath = true;
    [SerializeField] Camera AIcamera = null;

    private void Awake() => RegisterCallbacks();

    void RegisterCallbacks()
    {
        if(pathfollow && movement)
        {
            pathfollow.OnPathDestinationAcquired += movement.MoveToDestination;
            movement.OnDestinationReached += (Vector3 _destination) => pathfollow.GetNextPosition();
        }

        if(vision)
        {
            vision.OnPlayerSpotted += IALogicOnSpotPlayer;
        }
    }

    void IALogicOnSpotPlayer(Player _player)
    {
        if (movement) movement.StopMovement();
        Vector3 _AIToPlayer = _player.transform.position - transform.position;
        _AIToPlayer = Vector3.ProjectOnPlane(_AIToPlayer, transform.up).normalized;
        Quaternion _direction =  Quaternion.LookRotation(_AIToPlayer);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _direction, 360);
        Camera _playerCamera = _player.GetCamera();
        if (AIcamera && _playerCamera)
        {
           AIcamera.enabled = true;
        }
        StartCoroutine(GameManager.GameOver(null));
    }
}
