using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 불변하는 데이터를 저장하는 클래스
public class BaseStats
{
    public readonly string Name;

    public readonly int MaxHealth;
    public readonly int AttackPoint;
    public readonly int DefensePoint;
    public readonly int MoveDistance;

    public BaseStats(string name, int maxHealth, int attackPoint, int defensePoint, int moveDistance)
    {
        Name = name;
        MaxHealth = maxHealth;
        AttackPoint = attackPoint;
        DefensePoint = defensePoint;
        MoveDistance = moveDistance;
    }
}

// 게임 진행 중 변경되는 스탯을 저장하는 클래스 (BaseStats로부터 초기값을 받음)
public class CurrentStats
{
    public int MaxHealth { get; set; }
    public int Health { get; set; }
    public int AttackPoint { get; set; }
    public int DefensePoint { get; set; }
    public int MoveDistance { get; set; }

    public CurrentStats(BaseStats baseStats)
    {
        // 기본 스탯을 현재 스탯으로 초기화
        MaxHealth = baseStats.MaxHealth;
        Health = baseStats.MaxHealth;       // 현재 체력을 최대 체력으로 설정
        AttackPoint = baseStats.AttackPoint;
        DefensePoint = baseStats.DefensePoint;
        MoveDistance = baseStats.MoveDistance;
    }
}

// 각 유닛의 추상 클래스 (각 유닛은 이를 상속받아 구현됨)
public abstract class Unit : MonoBehaviour
{
    public BaseStats BaseStats { get; private set; }
    public CurrentStats CurrentStats { get; private set; }
    public int x_pos, y_pos;

    // 생성자 (유닛 생성 시 좌표를 지정해야 함)
    protected Unit(BaseStats baseStats, int x_pos, int y_pos)
    {
        BaseStats = baseStats;
        CurrentStats = new CurrentStats(baseStats);
        this.x_pos = x_pos;
        this.y_pos = y_pos;
    }

    // 좌표(행과 열)을 튜플로 반환
    public (int, int) GetPosition()
    {
        (int, int) position = (x_pos, y_pos);
        return position;
    }

    // 유닛의 좌표(행과 열)을 변경
    public void SetPosition(int x_pos, int y_pos)
    {
        this.x_pos = x_pos;
        this.y_pos = y_pos;
    }

    /*    // 테스트용 공격
        public virtual void attack(Unit target)
        {
            Debug.Log($"{this.name}이(가) 공격력 {this.CurrentStats.AttackPoint}로 {target.name}을(를) 공격했습니다!");

            if (this.CurrentStats.AttackPoint > target.CurrentStats.DefensePoint)
            {
                int realDamage = this.CurrentStats.AttackPoint - target.CurrentStats.DefensePoint;
                target.CurrentStats.Health -= realDamage;
                Debug.Log($"{target.name}의 현재 체력 : {target.CurrentStats.Health}");
            }
            else
            {
                Debug.Log($"{this.name}의 공격력보다 {target.name}의 수비력이 더 높습니다.");
            }
        }*/

    // 행동 선택 UI : [이동하기], [스킬1], [스킬2] UI
    // [이동하기] 버튼을 누르면 해당 함수를 호출
    // 행동 선택 UI를 닫고, 시각적으로 이동 가능한 타일의 색을 다르게 표시하여 출력 (Map의 GetReachableCoordinate() 함수 이용)
    // 이 곳이 아닌 UIManager에서 작성바람, 즉 move() 함수가 호출되기 직전에 이 기능이 수행되어야 함
    // 클릭을 통해 Tile 객체를 얻었다고 가정(즉, fromTile != null인 상태에서 호출)
    // fromTile : 현재 클릭을 통해 선택된 타일
    // toTile에 대해 : null 여부 확인 -> 유효 거리 확인 -> 이동 불가 타일 확인 -> 이동 수행 -> 턴 종료
    public virtual void move(BasicMap map, Tile fromTile)
    {


        Tile toTile = null;

        // 유효한 위치를 클릭할 때까지 마우스 클릭을 입력받음
        while (true)
        {
            // 이동할 타일을 좌클릭으로 클릭하여 해당 타일 객체를 받음 (Tile을 받은 경우 그 객체를 가져오도록 수정해야 함)
            // toTile = OnclickFunc();

            if (toTile == null)
            {
                // 타일이 아닌 오브젝트를 클릭한 경우
                print("타일을 클릭하세요.");
            }
            else if (toTile.tileType == TileType.Unreachable || toTile.unit != null)
            {
                // 이동 불가 타일(TileType.Unreachable)이거나 유닛이 이미 존재하는 타일인 경우
                print("이동 불가능한 타일입니다.");
            }
            else
            {
                // 이동 가능한 타일인 경우 -> 유닛의 이동 거리 내의 타일인지 확인
                //int toX =
                //int toY = position.Item2;

                // fromTile로부터 유효한 거리
                List<(int, int)> reachableCoorinates = map.GetReachableCoordinates(fromTile, this.CurrentStats.MoveDistance, (10, 10));
                
                break;
            }
        }
    }

    // 스킬은 각 캐릭터마다 다르므로 추상 메서드로 선언
    public abstract void castSkill1();
    public abstract void castSkill2();
}

// 각 유닛의 인터페이스
public interface IUnit
{
    void attack();
    void move();
    void castSkill1();   // 1번 스킬
    void castSkill2();   // 2번 스킬
}