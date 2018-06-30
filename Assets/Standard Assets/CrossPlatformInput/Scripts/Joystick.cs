using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UnityStandardAssets.CrossPlatformInput
{
	public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
	{
		public enum AxisOption
		{
			// Options for which axes to use
			Both, // Use both
			OnlyHorizontal, // Only horizontal
			OnlyVertical // Only vertical
		}
        
		public int MovementRange;
		public AxisOption axesToUse = AxisOption.Both; // The options for the axes that the still will use
		public string horizontalAxisName = "Horizontal"; // The name given to the horizontal axis for the cross platform input
		public string verticalAxisName = "Vertical"; // The name given to the vertical axis for the cross platform input

		Vector3 m_StartPos;
		bool m_UseX; // Toggle for using the x axis
		bool m_UseY; // Toggle for using the Y axis
		CrossPlatformInputManager.VirtualAxis m_HorizontalVirtualAxis; // Reference to the joystick in the cross platform input
		CrossPlatformInputManager.VirtualAxis m_VerticalVirtualAxis; // Reference to the joystick in the cross platform input

		private float diagonal;
		private float diagScalingFactor;

		void OnEnable()
		{
			CreateVirtualAxes();
		}

        void Start()
        {
			//CreateVirtualAxes();
            m_StartPos = transform.position;
			MovementRange = (int) GetComponent<RectTransform>().rect.height / 2;
        }

		void UpdateVirtualAxes(Vector3 value)
		{
			var delta = m_StartPos - value;
			delta.y = -delta.y;
			delta /= MovementRange;

			if (m_UseX)
			{
				m_HorizontalVirtualAxis.Update(-delta.x);
			}

			if (m_UseY)
			{
				m_VerticalVirtualAxis.Update(delta.y);
			}
		}

		void CreateVirtualAxes()
		{

			//print("registered already");

			// set axes to use
			m_UseX = (axesToUse == AxisOption.Both || axesToUse == AxisOption.OnlyHorizontal);
			m_UseY = (axesToUse == AxisOption.Both || axesToUse == AxisOption.OnlyVertical);

			//if (CrossPlatformInput.CrossPlatformInputManager.GetAxis(horizontalAxisName) != null){
			//	return;
			//}
			                  

			// create new axes based on axes to use
			if (m_UseX)
			{
				m_HorizontalVirtualAxis = new CrossPlatformInputManager.VirtualAxis(horizontalAxisName);
				CrossPlatformInputManager.RegisterVirtualAxis(m_HorizontalVirtualAxis);
			}
			if (m_UseY)
			{
				m_VerticalVirtualAxis = new CrossPlatformInputManager.VirtualAxis(verticalAxisName);
				CrossPlatformInputManager.RegisterVirtualAxis(m_VerticalVirtualAxis);
			}
		}

		private float[] CircleEdgePointComputate(
			float xPos, float xStart, float yPos, float yStart, float radius){
			return new float[] { };

		}


		public void OnDrag(PointerEventData data)
		{
			Vector3 newPos = Vector3.zero;



			float deltaX = data.position.x - m_StartPos.x;
			float deltaY = data.position.y - m_StartPos.y;

			diagonal = (float)Math.Sqrt(
				Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2));
			diagScalingFactor = MovementRange / diagonal;
            
			if(m_UseX){
				float clampDeltaX = Math.Abs(deltaX * diagScalingFactor);

				deltaX = Mathf.Clamp(
                    deltaX, -clampDeltaX,
                    clampDeltaX);
                newPos.x = deltaX;				
			}

			if (m_UseY){
				float clampDeltaY = Math.Abs(deltaY * diagScalingFactor);

				deltaY = Mathf.Clamp(
                    deltaY, -clampDeltaY,
                    clampDeltaY);
                newPos.y = deltaY;
			}
            
			transform.position = new Vector3(m_StartPos.x + newPos.x, m_StartPos.y + newPos.y, m_StartPos.z + newPos.z);
			UpdateVirtualAxes(transform.position);
		}


		public void OnPointerUp(PointerEventData data)
		{
			transform.position = m_StartPos;
			UpdateVirtualAxes(m_StartPos);
		}


		public void OnPointerDown(PointerEventData data) { }

		void OnDisable()
		{
			// remove the joysticks from the cross platform input
			if (m_UseX)
			{
				m_HorizontalVirtualAxis.Remove();
			}
			if (m_UseY)
			{
				m_VerticalVirtualAxis.Remove();
			}
		}
	}
}