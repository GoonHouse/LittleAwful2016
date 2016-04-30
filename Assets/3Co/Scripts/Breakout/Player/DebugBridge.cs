using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DebugBridge : MonoBehaviour {
    public GameObject player;
    public PlayerPaddle paddle;
    public Text ballNo;

    // Use this for initialization
    void Start() {
        paddle = player.GetComponent<PlayerPaddle>();
    }

    // Update is called once per frame
    void Update() {
        ballNo.text = System.String.Format("{0}/{1}", paddle.focusedBallIndex, paddle.spawnedBalls.Count-1);
    }

    int Mod(int a, int b) {
        return (a % b + b) % b;
    }

    public void TargetNudge(int direction) {
        var numBalls = paddle.spawnedBalls.Count;
        var newPos = (paddle.focusedBallIndex + direction);
        paddle.focusedBallIndex = Mod(newPos, numBalls);
        Debug.Log("OH BOY " + newPos.ToString() + " YEAH JACKSON " + numBalls.ToString() + " SHIT DOGGY " + paddle.focusedBallIndex.ToString());
        paddle.focusedBall = paddle.spawnedBalls[paddle.focusedBallIndex];
    }
}
