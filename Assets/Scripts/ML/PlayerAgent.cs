using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;
using static TileEnums;

public class PlayerAgent : Agent
{
    public static PlayerAgent Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public override void OnEpisodeBegin()
    {
        // 새로운 에피소드 시작 시 호출
        AIManager1.Instance.RandomDeploy();
        Debug.Log("PlayerAgent Episode Started");
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        foreach (var tile in MapManager.Instance.allTileInfos)
        {
            float teamValue = 0f;
            if (tile.unit != null)
            {
                teamValue = tile.unit.team == Team.Ally ? 1f : -1f;
            }

            sensor.AddObservation(teamValue); // 유닛 팀 정보 (Ally: 1, Enemy: -1, 없음: 0)
            sensor.AddObservation(tile.unit != null ? tile.unit.currentHealth : 0f); // 유닛 체력
        }
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        // 행동 수행
        int moveAction = actions.DiscreteActions[0];
        int attackAction = actions.DiscreteActions[1];

        // 예제: 행동 인덱스를 기반으로 간단한 Move/Attack 수행
        AIManager1.Instance.ProcessEnemyAction(moveAction, attackAction);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        // 디버그를 위한 휴리스틱 제공
        var discreteActions = actionsOut.DiscreteActions;
        discreteActions[0] = Random.Range(0, MapManager.Instance.allTileInfos.Count); // 임의의 이동
        discreteActions[1] = Random.Range(0, 2); // 공격 여부 선택
    }
}
