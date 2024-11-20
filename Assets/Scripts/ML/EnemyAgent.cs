using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;
using static TileEnums;

public class EnemyAgent : Agent
{
    public static EnemyAgent Instance { get; private set; }
    public Unit controlledUnit;

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
        AIManager1.Instance.RandomDeploy2();
        Debug.Log("EnemyAgent Episode Started");
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
        int action = actions.DiscreteActions[0]; // 단일 분지에서 하나의 행동 선택
        AIManager1.Instance.ProcessEnemyAction(action);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActions = actionsOut.DiscreteActions;

        // 유효한 유닛 인덱스를 랜덤으로 선택
        int unitIndex = Random.Range(0, AIManager1.Instance.GetUnitTiles().Count);

        // 행동 결정: 0 = 이동, 1 = 공격
        int isAttack = Random.Range(0, 2);

        // 단일 행동 값 생성
        discreteActions[0] = unitIndex * 2 + isAttack;
    }

}
