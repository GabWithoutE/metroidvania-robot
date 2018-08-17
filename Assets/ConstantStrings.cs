using UnityEngine;
using System.Collections;
public static class ConstantStrings
{
	public const string TIME_SCALE = "TimeScale";
    public const string CARDINAL_DIRECTION = "CardinalDirection";
	public const string TRANSFORMATION = "Transformation";
	public const string VELOCITY = "Velocity";
	public const string SPEED_SCALE = "SpeedScale";

    //public const string MOVE_SET = "MoveSet";
    public const string CURRENT_HEALTH = "CurrentHealth";
    public const string MAX_HEALTH = "MaxHealth";
    public const string DEATH_STATE = "DeathState";
    public const string HIT_STATE = "HitState";

    public const string LIGHT_ATTACK_COOLDOWN = "LightAttackCooldown";
	public const string HEAVY_ATTACK_COOLDOWN = "HeavyAttackCooldown";
	public const string UTILITY_COOLDOWN = "UtilityCooldown";

	public const string MAINGAME_SCENE_NAME = "MainGameSceneForREa";

	public const string HIT_ENEMY = "HitEnemy";
	public const string KILLED_ENEMY = "KilledEnemy";

	public const string SCORE = "Score";

	public const string JUMPING = "Jumping";
	public const string GROUNDED = "Grounded";

	public const string DIRECTION = "Direction";

	public const string LIGHT_ATTACK_CAST = "LightAttackCast";
	public const string HEAVY_ATTACK_CAST = "HeavyAttackCast";
	public const string UTILITY_ABILITY_CAST = "UtilityAbilityCast";

	public const string UTILITY_RESOURCE_STATE = "UtilityResourceState";

    public const string RECOIL_STATE = "Recoil";

    public const string MAGNET_STATE = "Magnet";

	public static class CameraDirectionBoxes{
		public const string STAGE_EDGE_HORIZONTAL_RIGHT = "StageEdgeHorizontalRight";
		public const string STAGE_EDGE_HORIZONTAL_LEFT = "StageEdgeHorizontalLeft";
		public const string STAGE_BOTTOM_LINE = "StageBottomLine";
		public const string LOCK_FOCUS_ON_AREA = "LockFocusOnArea";
		public const string STAGE_TOP_LINE = "StageTopLine";
	}
    
	public static class PlayerAnimatorStates{
		public const string IDLE_HORIZONTAL = "IdleHorizontal";
		public const string IDLE_VERTICAL = "IdleVertical";

		public const string LIGHT_ATTACK_ANIMATION = "LightAttackAnimation";
		public const string HEAVY_ATTACK_ANIMATION = "HeavyAttackAnimation";
		public const string UTILITY_ABILITY_ANIMATION = "UtilityAbilityAnimation";

		public const string RUNNING_HORIZONTAL = "RunningHorizontal";
        public const string RUNNING_VERTICAL = "RunningVertical";

		public const string AWAY = "Away";
	}
    


    public static class UI
    {
        public static class Input
        {
            public const string INPUT_HORIZONTAL = "InputHorizontal";
			public const string INPUT_VERTICAL = "InputVertical";
            public const string INPUT_LIGHT_ATTACK = "InputLightAttack";
			public const string INPUT_HEAVY_ATTACK = "InputHeavyAttack";
			public const string INPUT_UTILITY = "InputUtility";
			public const string INPUT_TRANSFORM = "InputTransform";
			public const string INPUT_JUMP = "InputJump";
        }

        public static class HUD
        {
            public const string HEALTH_BAR = "HealthBar";
            public const string PROFILE_PICTURE = "ProfilePicture";
        }

    }

    public static class Enemy
    {
        public static class HammerBoss
        {
            public const string HAMMER_THROWN = "HammerThrown";
            public const string HAMMER_HITS_GROUND = "HammerHitsGround";
            public const string HAMMER_HIT_PLAYER = "HammerHitPlayer";
            public const string HAMMER_HIT_ENEMY = "HammerHitEnemy";
            public const string THROWN_HAMMER_POSITION = "ThrownHammerPosition";
            public const string MELEE_ATTACK_CAST_STATE = "meleeAttackCastState";
            public const string HAMMER_THROW_CAST_STATE = "hammerThrowCastState";
            public const string JUMP_STATE = "jumpState";
            public const string BUSY_STATE = "busyState";
        }
        public const string STUN_STATE = "stunState";
    }

}

