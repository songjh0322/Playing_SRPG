using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using UnityEngine;

public class EnemyAgent : Agent
{
    public TileInfo currentTile;

    // 에피소드 초기화
    public override void OnEpisodeBegin()
    {
        AIManager.Instance.RandomDeploy();
    }

    // 환경 관찰 수집
    public override void CollectObservations(VectorSensor sensor)
    {
        // 유닛의 현재 위치와 상태를 관찰
        sensor.AddObservation(currentTile.unit.currentHealth);
        sensor.AddObservation(currentTile.unit.currentAttackPoint);
        sensor.AddObservation(currentTile.unit.currentDefensePoint);

        // 주변 타일 상태
        var nearbyTiles = MapManager.Instance.GetManhattanTiles(currentTile.gameObject, currentTile.unit.currentMoveRange);
        foreach (var tile in nearbyTiles)
        {
            sensor.AddObservation(tile.GetComponent<TileInfo>().unit != null ? 1.0f : 0.0f);
        }
    }

    // 행동 정의
    public override void OnActionReceived(ActionBuffers actions)
    {
        int action = actions.DiscreteActions[0];

        if (action == 0)
        {
            TryMove();  // 이동
        }
        else if (action == 1)
        {
            TryAttack();  // 공격
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        // 임시로 랜덤 행동 설정
        var discreteActions = actionsOut.DiscreteActions;
        discreteActions[0] = Random.Range(0, 2); // 0: 이동, 1: 공격
    }

    private void TryMove()
    {
        // 현재 유닛 이동 로직 호출
        AIManager.Instance.OnAITurnStarted();
    }

    private void TryAttack()
    {
        // 공격 수행 로직
        AIManager.Instance.OnAITurnStarted();
    }
}
