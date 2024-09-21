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
    public int x, y;

    // 생성자 (유닛 생성 시 좌표를 지정해야 함)
    protected Unit(BaseStats baseStats, int x, int y)
    {
        BaseStats = baseStats;
        CurrentStats = new CurrentStats(baseStats);
        this.x = x;
        this.y = y;
    }

    // 좌표(행과 열)을 반환
    public (int, int) GetPosition()
    {
        return (x, y);
    }

    // 유닛의 좌표(행과 열)을 변경
    public void SetPosition(int x, int y)
    {
        this.x = x;
        this.y = y;
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

    // 해당 유닛을 이미 선택한 상태에서 호출 ([이동하기] 버튼을 누르면 이 함수를 호출)
    // 모든 유닛이 같은 행동을 취함
    public virtual void move(BasicMap map)
    {
        Debug.Log("[이동하기] 버튼을 눌렀음");
        // 팝업창을 닫고, 시각적으로 이동 가능한 타일의 색을 다르게 표기

        Tile selected_tile = null;
        while (true)
        {
            // 이동할 타일을 좌클릭으로 클릭하여 해당 타일 객체를 받음 (Tile을 받은 경우 그 객체를 가져오도록 수정해야 함)
            selected_tile =  null;

            if (selected_tile == null)
            {
                // 클릭으로 인식한 오브젝트가 타일이 아닌 경우 : 아무것도 처리하지 않고 다음 클릭을 입력받음
                Debug.Log("이동 거리 내의 타일을 선택하세요.");
            }
            else if (selected_tile.tileType == TileType.Unreachable || selected_tile.unit != null)
            {
                // 이동 불가 타일 또는 유닛이 이미 있는 타일인 경우
                Debug.Log("이동 불가능한 타일이거나 이미 유닛이 존재하는 타일입니다.");
            }
            else
            {
                // 이동 가능한 타일인 경우
                List<(int, int)> list = map.GetReachableTiles(selected_tile, this.CurrentStats.MoveDistance, 10, 10);
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