using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class BattleRecord
{
	public class SlotInfo
	{
		public int Index;
		public string Charname;
		public int CharHealth;
		public int CharMaxHealth;

		public void Update ( Slot slot )
		{
			this.Index = slot.Index;
			this.Charname = slot.Character.PInfoStats.Name;
			this.CharHealth = slot.Character.PPointStats.Health.Current;
			this.CharMaxHealth = slot.Character.PPointStats.Health.Max;
		}
	}

	public int Turn;
	public bool IsLeft;
	public SlotInfo AttackerSlot = new SlotInfo();
	public SlotInfo DefenderSlot = new SlotInfo();
	public int Damage;
	public bool Dodged;
	public bool Blocked;
	public bool Crit;
}

public class BattleRecords : List<BattleRecord>
{

}

public class BattleRecordController  {

	private static BattleRecordController _instance = null;

	public static BattleRecordController Instance
	{
		get{
			if( null == _instance )
			{
				_instance = new BattleRecordController( );
			}

			return _instance;
		}
	}

	public BattleRecords PBattleRecords = new BattleRecords();
	public bool WinGameResult = false;

	private int CurrentIdx = 0;
	public void CreateRecord ( )
	{
		BattleRecord record = new BattleRecord( );
		PBattleRecords.Add(record);

		CurrentIdx = PBattleRecords.Count - 1;
	}

	public void Reset ( )
	{
		PBattleRecords.Clear( );
	}

	public BattleRecord GetCurrentRecord ( )
	{
		if( CurrentIdx < 0 || CurrentIdx >= PBattleRecords.Count )
			return null;

		return PBattleRecords[CurrentIdx];
	}

	public static void RecordTurn ( int turn )
	{
		BattleRecord record = BattleRecordController.Instance.GetCurrentRecord();
		if( record != null )
		{
			record.Turn = turn;
		}
	}

	public static void RecrodIsLeft ( bool isLeft )
	{
		BattleRecord record = BattleRecordController.Instance.GetCurrentRecord();
		if( record != null )
		{
			record.IsLeft = isLeft;
		}
	}

	public static void RecordSlot ( Slot attackerSlot , Slot defenderSlot )
	{
		BattleRecord record = BattleRecordController.Instance.GetCurrentRecord();
		if( record != null )
		{
			record.AttackerSlot.Update(attackerSlot);
			record.DefenderSlot.Update(defenderSlot);
		}
	}

	public static void RecordAttackInfo ( int damage , bool dodge , bool blocked , bool crit )
	{
		BattleRecord record = BattleRecordController.Instance.GetCurrentRecord();
		if( record != null )
		{
			record.Damage = damage;
			record.Dodged = dodge;
			record.Blocked = blocked;
			record.Crit = crit;
		}
	}
}
