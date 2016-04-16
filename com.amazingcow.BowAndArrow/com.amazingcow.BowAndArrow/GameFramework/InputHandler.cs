using System;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;


namespace com.amazingcow.BowAndArrow
{
	public class ActionStep
	{
		public int Button;
		public ButtonState State;

		public bool Check(MouseState state)
		{
			return state.LeftButton == State;
		}

		public override string ToString ()
		{
			return string.Format("[ActionStep] Button:{0} - State:{1}", 
								 Button, State);
		}
	}

	public class Action
	{
		public List<ActionStep> Steps = new List<ActionStep>();
		public int StepIndex = 0;

		public event EventHandler<EventArgs> OnTrigger;

		public void Check(MouseState state)
		{					
		}
	}

	public class InputHandler
	{
		#region Singleton 
		private static InputHandler s_instance = null;
		public static InputHandler Instance
		{
			get { 
				if(s_instance == null)
					s_instance = new InputHandler();
				return s_instance;
			}
		}
		#endregion

		public List<Action> Actions = new List<Action>();

		#region CTOR 
		private InputHandler()
		{
		}
		#endregion


		#region Update 
		public void Update()
		{				
			var state = Mouse.GetState();

			foreach(var action in Actions)
				action.Check(state);
		}
		#endregion

	}
}

