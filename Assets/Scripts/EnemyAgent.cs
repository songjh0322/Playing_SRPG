using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using UnityEngine;

public class EnemtAgent : Agent
{
    public override void OnEpisodeBegin()
    {
        // 에피소드 초기화 (필요에 따라 맵 초기화 등 추가)
        Debug.Log("Episode Started");
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // 간단한 관찰값 추가 (테스트용)
        sensor.AddObservation(Random.Range(0f, 1f));  // 랜덤 값으로 테스트
        Debug.Log("Observations Collected");
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        // 행동 수행 (테스트용)
        int action = actions.DiscreteActions[0];
        Debug.Log($"Action Received: {action}");
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        // 수동 테스트 (랜덤 행동 수행)
        var discreteActions = actionsOut.DiscreteActions;
        discreteActions[0] = Random.Range(0, 3);  // 3개의 행동 중 하나를 랜덤 선택
    }
}
