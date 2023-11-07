using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotcamComponent : MonoBehaviour
{
    [SerializeField] Camera spotCamera = null;

    public void ActivateSpotCamera()
    {
        if (!spotCamera || GameManager.gameIsOver) return;
        spotCamera.enabled = true;
    }

    public void FollowPlayer(Player _player)
    {
        if (!spotCamera.enabled) return;
        Vector3 _AIToPlayer = _player.transform.position - transform.position;
        _AIToPlayer = Vector3.ProjectOnPlane(_AIToPlayer, transform.up).normalized;
        Quaternion _direction = Quaternion.LookRotation(_AIToPlayer);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _direction, 360);
    }
}
