using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x02000724 RID: 1828
[AddComponentMenu("KMonoBehaviour/Workable/DoctorStation")]
public class DoctorStation : Workable
{
	// Token: 0x06003204 RID: 12804 RVA: 0x0010BB50 File Offset: 0x00109D50
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
	}

	// Token: 0x06003205 RID: 12805 RVA: 0x0010BB58 File Offset: 0x00109D58
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Prioritizable.AddRef(base.gameObject);
		this.doctor_workable = base.GetComponent<DoctorStationDoctorWorkable>();
		base.SetWorkTime(float.PositiveInfinity);
		this.smi = new DoctorStation.StatesInstance(this);
		this.smi.StartSM();
		this.OnStorageChange(null);
		base.Subscribe<DoctorStation>(-1697596308, DoctorStation.OnStorageChangeDelegate);
	}

	// Token: 0x06003206 RID: 12806 RVA: 0x0010BBBC File Offset: 0x00109DBC
	protected override void OnCleanUp()
	{
		Prioritizable.RemoveRef(base.gameObject);
		if (this.smi != null)
		{
			this.smi.StopSM("OnCleanUp");
			this.smi = null;
		}
		base.OnCleanUp();
	}

	// Token: 0x06003207 RID: 12807 RVA: 0x0010BBF0 File Offset: 0x00109DF0
	private void OnStorageChange(object data = null)
	{
		this.treatments_available.Clear();
		foreach (GameObject gameObject in this.storage.items)
		{
			MedicinalPill component = gameObject.GetComponent<MedicinalPill>();
			if (component != null)
			{
				Tag tag = gameObject.PrefabID();
				foreach (string text in component.info.curedSicknesses)
				{
					this.AddTreatment(text, tag);
				}
			}
		}
		bool flag = this.treatments_available.Count > 0;
		this.smi.sm.hasSupplies.Set(flag, this.smi, false);
	}

	// Token: 0x06003208 RID: 12808 RVA: 0x0010BCE0 File Offset: 0x00109EE0
	private void AddTreatment(string id, Tag tag)
	{
		if (!this.treatments_available.ContainsKey(id))
		{
			this.treatments_available.Add(id, tag);
		}
	}

	// Token: 0x06003209 RID: 12809 RVA: 0x0010BD07 File Offset: 0x00109F07
	protected override void OnStartWork(Worker worker)
	{
		base.OnStartWork(worker);
		this.smi.sm.hasPatient.Set(true, this.smi, false);
	}

	// Token: 0x0600320A RID: 12810 RVA: 0x0010BD2E File Offset: 0x00109F2E
	protected override void OnStopWork(Worker worker)
	{
		base.OnStopWork(worker);
		this.smi.sm.hasPatient.Set(false, this.smi, false);
	}

	// Token: 0x0600320B RID: 12811 RVA: 0x0010BD55 File Offset: 0x00109F55
	public override bool InstantlyFinish(Worker worker)
	{
		return false;
	}

	// Token: 0x0600320C RID: 12812 RVA: 0x0010BD58 File Offset: 0x00109F58
	public void SetHasDoctor(bool has)
	{
		this.smi.sm.hasDoctor.Set(has, this.smi, false);
	}

	// Token: 0x0600320D RID: 12813 RVA: 0x0010BD78 File Offset: 0x00109F78
	public void CompleteDoctoring()
	{
		if (!base.worker)
		{
			return;
		}
		this.CompleteDoctoring(base.worker.gameObject);
	}

	// Token: 0x0600320E RID: 12814 RVA: 0x0010BD9C File Offset: 0x00109F9C
	private void CompleteDoctoring(GameObject target)
	{
		Sicknesses sicknesses = target.GetSicknesses();
		if (sicknesses != null)
		{
			bool flag = false;
			foreach (SicknessInstance sicknessInstance in sicknesses)
			{
				Tag tag;
				if (this.treatments_available.TryGetValue(sicknessInstance.Sickness.id, out tag))
				{
					Game.Instance.savedInfo.curedDisease = true;
					sicknessInstance.Cure();
					this.storage.ConsumeIgnoringDisease(tag, 1f);
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				global::Debug.LogWarningFormat(base.gameObject, "Failed to treat any disease for {0}", new object[] { target });
			}
		}
	}

	// Token: 0x0600320F RID: 12815 RVA: 0x0010BE50 File Offset: 0x0010A050
	public bool IsDoctorAvailable(GameObject target)
	{
		if (!string.IsNullOrEmpty(this.doctor_workable.requiredSkillPerk))
		{
			MinionResume component = target.GetComponent<MinionResume>();
			if (!MinionResume.AnyOtherMinionHasPerk(this.doctor_workable.requiredSkillPerk, component))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06003210 RID: 12816 RVA: 0x0010BE8C File Offset: 0x0010A08C
	public bool IsTreatmentAvailable(GameObject target)
	{
		Sicknesses sicknesses = target.GetSicknesses();
		if (sicknesses != null)
		{
			foreach (SicknessInstance sicknessInstance in sicknesses)
			{
				Tag tag;
				if (this.treatments_available.TryGetValue(sicknessInstance.Sickness.id, out tag))
				{
					return true;
				}
			}
			return false;
		}
		return false;
	}

	// Token: 0x04001E5C RID: 7772
	private static readonly EventSystem.IntraObjectHandler<DoctorStation> OnStorageChangeDelegate = new EventSystem.IntraObjectHandler<DoctorStation>(delegate(DoctorStation component, object data)
	{
		component.OnStorageChange(data);
	});

	// Token: 0x04001E5D RID: 7773
	[MyCmpReq]
	public Storage storage;

	// Token: 0x04001E5E RID: 7774
	[MyCmpReq]
	public Operational operational;

	// Token: 0x04001E5F RID: 7775
	private DoctorStationDoctorWorkable doctor_workable;

	// Token: 0x04001E60 RID: 7776
	private Dictionary<HashedString, Tag> treatments_available = new Dictionary<HashedString, Tag>();

	// Token: 0x04001E61 RID: 7777
	private DoctorStation.StatesInstance smi;

	// Token: 0x04001E62 RID: 7778
	public static readonly Chore.Precondition TreatmentAvailable = new Chore.Precondition
	{
		id = "TreatmentAvailable",
		description = DUPLICANTS.CHORES.PRECONDITIONS.TREATMENT_AVAILABLE,
		fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			return ((DoctorStation)data).IsTreatmentAvailable(context.consumerState.gameObject);
		}
	};

	// Token: 0x04001E63 RID: 7779
	public static readonly Chore.Precondition DoctorAvailable = new Chore.Precondition
	{
		id = "DoctorAvailable",
		description = DUPLICANTS.CHORES.PRECONDITIONS.DOCTOR_AVAILABLE,
		fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			return ((DoctorStation)data).IsDoctorAvailable(context.consumerState.gameObject);
		}
	};

	// Token: 0x02001425 RID: 5157
	public class States : GameStateMachine<DoctorStation.States, DoctorStation.StatesInstance, DoctorStation>
	{
		// Token: 0x06008031 RID: 32817 RVA: 0x002DE9DC File Offset: 0x002DCBDC
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			base.serializable = StateMachine.SerializeType.Never;
			default_state = this.unoperational;
			this.unoperational.EventTransition(GameHashes.OperationalChanged, this.operational, (DoctorStation.StatesInstance smi) => smi.master.operational.IsOperational);
			this.operational.EventTransition(GameHashes.OperationalChanged, this.unoperational, (DoctorStation.StatesInstance smi) => !smi.master.operational.IsOperational).DefaultState(this.operational.not_ready);
			this.operational.not_ready.ParamTransition<bool>(this.hasSupplies, this.operational.ready, (DoctorStation.StatesInstance smi, bool p) => p);
			this.operational.ready.DefaultState(this.operational.ready.idle).ToggleRecurringChore(new Func<DoctorStation.StatesInstance, Chore>(this.CreatePatientChore), null).ParamTransition<bool>(this.hasSupplies, this.operational.not_ready, (DoctorStation.StatesInstance smi, bool p) => !p);
			this.operational.ready.idle.ParamTransition<bool>(this.hasPatient, this.operational.ready.has_patient, (DoctorStation.StatesInstance smi, bool p) => p);
			this.operational.ready.has_patient.ParamTransition<bool>(this.hasPatient, this.operational.ready.idle, (DoctorStation.StatesInstance smi, bool p) => !p).DefaultState(this.operational.ready.has_patient.waiting).ToggleRecurringChore(new Func<DoctorStation.StatesInstance, Chore>(this.CreateDoctorChore), null);
			this.operational.ready.has_patient.waiting.ParamTransition<bool>(this.hasDoctor, this.operational.ready.has_patient.being_treated, (DoctorStation.StatesInstance smi, bool p) => p);
			this.operational.ready.has_patient.being_treated.ParamTransition<bool>(this.hasDoctor, this.operational.ready.has_patient.waiting, (DoctorStation.StatesInstance smi, bool p) => !p).Enter(delegate(DoctorStation.StatesInstance smi)
			{
				smi.GetComponent<Operational>().SetActive(true, false);
			}).Exit(delegate(DoctorStation.StatesInstance smi)
			{
				smi.GetComponent<Operational>().SetActive(false, false);
			});
		}

		// Token: 0x06008032 RID: 32818 RVA: 0x002DECD4 File Offset: 0x002DCED4
		private Chore CreatePatientChore(DoctorStation.StatesInstance smi)
		{
			WorkChore<DoctorStation> workChore = new WorkChore<DoctorStation>(Db.Get().ChoreTypes.GetDoctored, smi.master, null, true, null, null, null, false, null, false, true, null, false, true, false, PriorityScreen.PriorityClass.personalNeeds, 5, false, true);
			workChore.AddPrecondition(DoctorStation.TreatmentAvailable, smi.master);
			workChore.AddPrecondition(DoctorStation.DoctorAvailable, smi.master);
			return workChore;
		}

		// Token: 0x06008033 RID: 32819 RVA: 0x002DED30 File Offset: 0x002DCF30
		private Chore CreateDoctorChore(DoctorStation.StatesInstance smi)
		{
			DoctorStationDoctorWorkable component = smi.master.GetComponent<DoctorStationDoctorWorkable>();
			return new WorkChore<DoctorStationDoctorWorkable>(Db.Get().ChoreTypes.Doctor, component, null, true, null, null, null, false, null, false, true, null, false, true, false, PriorityScreen.PriorityClass.high, 5, false, true);
		}

		// Token: 0x040062A5 RID: 25253
		public GameStateMachine<DoctorStation.States, DoctorStation.StatesInstance, DoctorStation, object>.State unoperational;

		// Token: 0x040062A6 RID: 25254
		public DoctorStation.States.OperationalStates operational;

		// Token: 0x040062A7 RID: 25255
		public StateMachine<DoctorStation.States, DoctorStation.StatesInstance, DoctorStation, object>.BoolParameter hasSupplies;

		// Token: 0x040062A8 RID: 25256
		public StateMachine<DoctorStation.States, DoctorStation.StatesInstance, DoctorStation, object>.BoolParameter hasPatient;

		// Token: 0x040062A9 RID: 25257
		public StateMachine<DoctorStation.States, DoctorStation.StatesInstance, DoctorStation, object>.BoolParameter hasDoctor;

		// Token: 0x02002045 RID: 8261
		public class OperationalStates : GameStateMachine<DoctorStation.States, DoctorStation.StatesInstance, DoctorStation, object>.State
		{
			// Token: 0x04008FDA RID: 36826
			public GameStateMachine<DoctorStation.States, DoctorStation.StatesInstance, DoctorStation, object>.State not_ready;

			// Token: 0x04008FDB RID: 36827
			public DoctorStation.States.ReadyStates ready;
		}

		// Token: 0x02002046 RID: 8262
		public class ReadyStates : GameStateMachine<DoctorStation.States, DoctorStation.StatesInstance, DoctorStation, object>.State
		{
			// Token: 0x04008FDC RID: 36828
			public GameStateMachine<DoctorStation.States, DoctorStation.StatesInstance, DoctorStation, object>.State idle;

			// Token: 0x04008FDD RID: 36829
			public DoctorStation.States.PatientStates has_patient;
		}

		// Token: 0x02002047 RID: 8263
		public class PatientStates : GameStateMachine<DoctorStation.States, DoctorStation.StatesInstance, DoctorStation, object>.State
		{
			// Token: 0x04008FDE RID: 36830
			public GameStateMachine<DoctorStation.States, DoctorStation.StatesInstance, DoctorStation, object>.State waiting;

			// Token: 0x04008FDF RID: 36831
			public GameStateMachine<DoctorStation.States, DoctorStation.StatesInstance, DoctorStation, object>.State being_treated;
		}
	}

	// Token: 0x02001426 RID: 5158
	public class StatesInstance : GameStateMachine<DoctorStation.States, DoctorStation.StatesInstance, DoctorStation, object>.GameInstance
	{
		// Token: 0x06008035 RID: 32821 RVA: 0x002DED77 File Offset: 0x002DCF77
		public StatesInstance(DoctorStation master)
			: base(master)
		{
		}
	}
}
