using UnityEngine;
using System.Collections;
public static class ConstantStrings
{
    public static readonly string TIME_SCALE = "TimeScale";
    public static readonly string CARDINAL_DIRECTION = "CardinalDirection";
	public static readonly string TRANSFORMATION = "Transformation";
	public static readonly string VELOCITY = "Velocity";
	public static readonly string SPEED_SCALE = "SpeedScale";

    //public static readonly string MOVE_SET = "MoveSet";
    public static readonly string CURRENT_HEALTH = "CurrentHealth";
    public static readonly string MAX_HEALTH = "MaxHealth";
    public static readonly string DEATH_STATE = "DeathState";

	public static readonly string LIGHT_ATTACK_COOLDOWN = "LightAttackCooldown";
	public static readonly string HEAVY_ATTACK_COOLDOWN = "HeavyAttackCooldown";
	public static readonly string UTILITY_COOLDOWN = "UtilityCooldown";

	public static readonly string MAINGAME_SCENE_NAME = "MainGameSceneForREa";

	public static readonly string HIT_ENEMY = "HitEnemy";
	public static readonly string KILLED_ENEMY = "KilledEnemy";

	public static readonly string SCORE = "Score";
    
	public static class PlayerAnimatorStates{
		public static readonly string IDLE_HORIZONTAL = "IdleHorizontal";
		public static readonly string IDLE_VERTICAL = "IdleVertical";

		public static readonly string LIGHT_ATTACK_ANIMATION = "LightAttackAnimation";
		public static readonly string HEAVY_ATTACK_ANIMATION = "HeavyAttackAnimation";
		public static readonly string UTILITY_ABILITY_ANIMATION = "UtilityAbilityAnimation";

		public static readonly string RUNNING_HORIZONTAL = "RunningHorizontal";
        public static readonly string RUNNING_VERTICAL = "RunningVertical";

		public static readonly string AWAY = "Away";
	}
    


    public static class UI
    {
        public static class Input
        {
            public static readonly string INPUT_HORIZONTAL = "InputHorizontal";
			public static readonly string INPUT_VERTICAL = "InputVertical";
            public static readonly string BUTTON_LIGHT_ATTACK = "ButtonLightAttack";
            public static readonly string BUTTON_HEAVY_ATTACK = "ButtonHeavyAttack";
            public static readonly string BUTTON_UTILITY = "ButtonUtility";
			public static readonly string BUTTON_TRANSFORM = "ButtonTransform";
        }

        public static class HUD
        {
            public static readonly string HEALTH_BAR = "HealthBar";
            public static readonly string PROFILE_PICTURE = "ProfilePicture";
        }

    }

}

