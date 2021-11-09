using System.Collections.Generic;
using static WeaponThread.WeaponStructure;
using static WeaponThread.WeaponStructure.WeaponDefinition;
using static WeaponThread.WeaponStructure.WeaponDefinition.HardPointDef;
using static WeaponThread.WeaponStructure.WeaponDefinition.ModelAssignmentsDef;
using static WeaponThread.WeaponStructure.WeaponDefinition.HardPointDef.HardwareDef.ArmorState;
using static WeaponThread.WeaponStructure.WeaponDefinition.HardPointDef.Prediction;
using static WeaponThread.WeaponStructure.WeaponDefinition.TargetingDef.BlockTypes;
using static WeaponThread.WeaponStructure.WeaponDefinition.TargetingDef.Threat;

namespace WeaponThread 
{ 
    partial class Weapons 
	{
		
		WeaponDefinition LancerTurret45 => new WeaponDefinition {
            Assignments = new ModelAssignmentsDef
            {
                MountPoints = new[]
                {
                    new MountPointDef
                    {
                        SubtypeId = "LancerTurret45",
                        AimPartId = "None",
                        MuzzlePartId = "MissileTurretBarrels",
                        AzimuthPartId = "MissileTurretBase1",
                        ElevationPartId = "MissileTurretBarrels",
                        DurabilityMod = 1f,
                        IconName = "None",
                    },
					new MountPointDef
                    {
                        SubtypeId = "LancerTurret30",
                        AimPartId = "None",
                        MuzzlePartId = "MissileTurretBarrels",
                        AzimuthPartId = "MissileTurretBase1",
                        ElevationPartId = "MissileTurretBarrels",
                        DurabilityMod = 1f,
                        IconName = "None",
                    },
					new MountPointDef
                    {
                        SubtypeId = "LargeLancerTurret45",
                        AimPartId = "None",
                        MuzzlePartId = "MissileTurretBarrels",
                        AzimuthPartId = "MissileTurretBase1",
                        ElevationPartId = "MissileTurretBarrels",
                        DurabilityMod = 1f,
                        IconName = "None",
                    },
					new MountPointDef
                    {
                        SubtypeId = "LargeLancerTurret30",
                        AimPartId = "None",
                        MuzzlePartId = "MissileTurretBarrels",
                        AzimuthPartId = "MissileTurretBase1",
                        ElevationPartId = "MissileTurretBarrels",
                        DurabilityMod = 1f,
                        IconName = "None",
                    },

                },
                Barrels = new []
                {
					"muzzle_missile_001",
					"muzzle_missile_002",
					"muzzle_missile_003",
					"muzzle_missile_004",
                },
            },
            Targeting = new TargetingDef
            {
                Threats = new[]
                {
                    Grids, Characters, Projectiles, Meteors, // threats percieved automatically without changing menu settings
                },
                SubSystems = new[]
                {
                    Thrust, Utility, Offense, Power, Production, Any, // subsystems the gun targets
                },
                ClosestFirst = false, // tries to pick closest targets first (blocks on grids, projectiles, etc...).
                MinimumDiameter = 5, // 0 = unlimited, Minimum radius of threat to engage.
                MaximumDiameter = 0, // 0 = unlimited, Maximum radius of threat to engage.
                TopTargets = 4, // 0 = unlimited, max number of top targets to randomize between.
                TopBlocks = 4, // 0 = unlimited, max number of blocks to randomize between
				MaxTargetDistance = 1000,
                StopTrackingSpeed = 1000, // do not track target threats traveling faster than this speed
            },
            HardPoint = new HardPointDef
            {
                WeaponName = "Lancer Turret", // name of weapon in terminal
                DeviateShotAngle = 0.85f,
                AimingTolerance = 1f, // 0 - 180 firing angle
                AimLeadingPrediction = Advanced, // Off, Basic, Accurate, Advanced
                DelayCeaseFire = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).

                Ui = new UiDef
                {
                    RateOfFire = true,
                    DamageModifier = false,
                    ToggleGuidance = false,
                    EnableOverload = false,
                },
                Ai = new AiDef
                {
                    TrackTargets = true,
                    TurretAttached = true,
                    TurretController = true,
                    PrimaryTracking = true,
                    LockOnFocus = false,
                },
                HardWare = new HardwareDef
                {
                    RotateRate = 0.3f,
                    ElevateRate = 0.3f,
                    MinAzimuth = -180,
                    MaxAzimuth = 180,
                    MinElevation = -10,
                    MaxElevation = 70,
                    FixedOffset = false,
                    InventorySize = 1f,
                    Offset = Vector(x: 0, y: 0, z:0),
                },
                Other = new OtherDef
                {
                    GridWeaponCap = 0,
                    RotateBarrelAxis = 0,
                    EnergyPriority = 0,
                    MuzzleCheck = false,
                    Debug = false,
                },
                Loading = new LoadingDef {
                    RateOfFire = 600,
                    BarrelSpinRate = 0, 
                    BarrelsPerShot = 4,
                    TrajectilesPerBarrel = 1, 
                    SkipBarrels = 0,
                    ReloadTime = 180, 
                    DelayUntilFire = 0, 
                    HeatPerShot = 0, 
                    MaxHeat = 0, 
                    Cooldown = 0, 
                    HeatSinkRate = 0, 
                    DegradeRof = false, 
                    ShotsInBurst = 2,
                    DelayAfterBurst = 20, 
                    FireFullBurst = true,
                    GiveUpAfterBurst = false,
                    DeterministicSpin = false, // Spin barrel position will always be relative to initial / starting positions (spin will not be as smooth).
                },
                Audio = new HardPointAudioDef {
                    PreFiringSound = "",
                    FiringSound = "LancerGunShot", // WepShipGatlingShot
                    FiringSoundPerShot = true,
                    ReloadSound = "LancerReload",
                    NoAmmoSound = "ArcWepShipGatlingNoAmmo",
                    HardPointRotationSound = "WepTurretGatlingRotate",
                    BarrelRotationSound = "WepShipGatlingRotation",
                    FireSoundEndDelay = 120, // Measured in game ticks(6 = 100ms, 60 = 1 seconds, etc..).
                },
                Graphics = new HardPointParticleDef {

					Barrel1 = new ParticleDef //FIXTHIS
                    {
                        Name = "AkiadFlash", // Smoke_LargeGunShot
                        Color = Color(red: 1, green: 1, blue: 1, alpha: 1),
                        Offset = Vector(x: 0, y: 0, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = true,
                            MaxDistance = 500,
                            MaxDuration = 1,
                            Scale = 1f,
                        },
                    },
                },
            },
            Ammos = new [] {
                Lancer50standard,Lancer50AP,
            },
             Animations= Lancer_Recoil
            // Don't edit below this line
        };
		
		
		WeaponDefinition SaberTurret45 => new WeaponDefinition {
            Assignments = new ModelAssignmentsDef
            {
                MountPoints = new[]
                {
                    new MountPointDef
                    {
                        SubtypeId = "SaberTurret45",
                        AimPartId = "None",
                        MuzzlePartId = "MissileTurretBarrels",
                        AzimuthPartId = "MissileTurretBase1",
                        ElevationPartId = "MissileTurretBarrels",
                        DurabilityMod = 1f,
                        IconName = "None",
                    },
					new MountPointDef
                    {
                        SubtypeId = "SaberTurret30",
                        AimPartId = "None",
                        MuzzlePartId = "MissileTurretBarrels",
                        AzimuthPartId = "MissileTurretBase1",
                        ElevationPartId = "MissileTurretBarrels",
                        DurabilityMod = 1f,
                        IconName = "None",
                    },
					new MountPointDef
                    {
                        SubtypeId = "LargeSaberTurret45",
                        AimPartId = "None",
                        MuzzlePartId = "MissileTurretBarrels",
                        AzimuthPartId = "MissileTurretBase1",
                        ElevationPartId = "MissileTurretBarrels",
                        DurabilityMod = 1f,
                        IconName = "None",
                    },
					new MountPointDef
                    {
                        SubtypeId = "LargeSaberTurret30",
                        AimPartId = "None",
                        MuzzlePartId = "MissileTurretBarrels",
                        AzimuthPartId = "MissileTurretBase1",
                        ElevationPartId = "MissileTurretBarrels",
                        DurabilityMod = 1f,
                        IconName = "None",
                    },

                },
                Barrels = new []
                {
					"muzzle_missile_001",
					"muzzle_missile_002",
                },
            },
            Targeting = new TargetingDef
            {
                Threats = new[]
                {
                    Grids, Characters, Projectiles, Meteors, // threats percieved automatically without changing menu settings
                },
                SubSystems = new[]
                {
                    Thrust, Utility, Offense, Power, Production, Any, // subsystems the gun targets
                },
                ClosestFirst = false, // tries to pick closest targets first (blocks on grids, projectiles, etc...).
                MinimumDiameter = 5, // 0 = unlimited, Minimum radius of threat to engage.
                MaximumDiameter = 0, // 0 = unlimited, Maximum radius of threat to engage.
                TopTargets = 4, // 0 = unlimited, max number of top targets to randomize between.
                TopBlocks = 4, // 0 = unlimited, max number of blocks to randomize between
				MaxTargetDistance = 1500,
                StopTrackingSpeed = 1000, // do not track target threats traveling faster than this speed
            },
            HardPoint = new HardPointDef
            {
                WeaponName = "Saber Turret", // name of weapon in terminal
                DeviateShotAngle = 0.85f,
                AimingTolerance = 1f, // 0 - 180 firing angle
                AimLeadingPrediction = Advanced, // Off, Basic, Accurate, Advanced
                DelayCeaseFire = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).

                Ui = new UiDef
                {
                    RateOfFire = true,
                    DamageModifier = false,
                    ToggleGuidance = false,
                    EnableOverload = false,
                },
                Ai = new AiDef
                {
                    TrackTargets = true,
                    TurretAttached = true,
                    TurretController = true,
                    PrimaryTracking = true,
                    LockOnFocus = false,
                },
                HardWare = new HardwareDef
                {
                    RotateRate = 0.3f,
                    ElevateRate = 0.3f,
                    MinAzimuth = -180,
                    MaxAzimuth = 180,
                    MinElevation = -10,
                    MaxElevation = 70,
                    FixedOffset = false,
                    InventorySize = 1f,
                    Offset = Vector(x: 0, y: 0, z:0),
                },
                Other = new OtherDef
                {
                    GridWeaponCap = 0,
                    RotateBarrelAxis = 0,
                    EnergyPriority = 0,
                    MuzzleCheck = false,
                    Debug = false,
                },
                Loading = new LoadingDef
                {
                    RateOfFire = 120,
                    BarrelSpinRate = 0, // visual only, 0 disables and uses RateOfFire
                    BarrelsPerShot = 1,
                    TrajectilesPerBarrel = 1, // Number of Trajectiles per barrel per fire event.
                    SkipBarrels = 0,
                    ReloadTime = 200, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    DelayUntilFire = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    HeatPerShot = 10, //heat generated per shot
                    MaxHeat = 1000000, //max heat before weapon enters cooldown (70% of max heat)
                    Cooldown = .95f, //percent of max heat to be under to start firing again after overheat accepts .2-.95
                    HeatSinkRate = 10, //amount of heat lost per second
                    DegradeRof = false, // progressively lower rate of fire after 80% heat threshold (80% of max heat)
                    ShotsInBurst = 4,
                    DelayAfterBurst = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    FireFullBurst = false,
                },
                Audio = new HardPointAudioDef
                {
                    PreFiringSound = "",
                    FiringSound = "SaberShot", // subtype name from sbc
                    FiringSoundPerShot = true,
                    ReloadSound = "LancerReload",
                    NoAmmoSound = "ArcWepShipGatlingNoAmmo",
                    HardPointRotationSound = "WepTurretGatlingRotate",
                    BarrelRotationSound = "",
                },
                Graphics = new HardPointParticleDef
                {
                    Barrel1 = new ParticleDef
                    {
                        Name = "AkiadFlash", // Smoke_LargeGunShot
                        Color = Color(red: 1, green: 1, blue: 1, alpha: 1),
                        Offset = Vector(x: 0, y: -1, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 200,
                            MaxDuration = 1,
                            Scale = 1.0f,
                        },
                    },
                    Barrel2 = new ParticleDef
                    {
                        Name = "AkiadFlash",//Muzzle_Flash_Large
                        Color = Color(red: 1, green: 1, blue: 1, alpha: 1),
                        Offset = Vector(x: 0, y: -1, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 100,
                            MaxDuration = 1,
                            Scale = 1f,
                        },
                    },
                },
            },

			Ammos = new [] {
                Lancer50AP,
            },
             Animations = SaberTurret_Recoil
            // Don't edit below this line
        };
		
		
		WeaponDefinition GlaiveTurret45 => new WeaponDefinition {
            Assignments = new ModelAssignmentsDef
            {
                MountPoints = new[]
                {
                    new MountPointDef
                    {
                        SubtypeId = "GlaiveTurret45",
                        AimPartId = "None",
                        MuzzlePartId = "MissileTurretBarrels",
                        AzimuthPartId = "MissileTurretBase1",
                        ElevationPartId = "MissileTurretBarrels",
                        DurabilityMod = 1f,
                        IconName = "None",
                    },
					new MountPointDef
                    {
                        SubtypeId = "GlaiveTurret30",
                        AimPartId = "None",
                        MuzzlePartId = "MissileTurretBarrels",
                        AzimuthPartId = "MissileTurretBase1",
                        ElevationPartId = "MissileTurretBarrels",
                        DurabilityMod = 1f,
                        IconName = "None",
                    },
					new MountPointDef
                    {
                        SubtypeId = "LargeGlaiveTurret45",
                        AimPartId = "None",
                        MuzzlePartId = "MissileTurretBarrels",
                        AzimuthPartId = "MissileTurretBase1",
                        ElevationPartId = "MissileTurretBarrels",
                        DurabilityMod = 1f,
                        IconName = "None",
                    },
					new MountPointDef
                    {
                        SubtypeId = "LargeGlaiveTurret30",
                        AimPartId = "None",
                        MuzzlePartId = "MissileTurretBarrels",
                        AzimuthPartId = "MissileTurretBase1",
                        ElevationPartId = "MissileTurretBarrels",
                        DurabilityMod = 1f,
                        IconName = "None",
                    },

                },
                Barrels = new []
                {
					"muzzle_missile_001",
                },
            },
            Targeting = new TargetingDef
            {
                Threats = new[]
                {
                    Grids, Characters, Projectiles, Meteors, // threats percieved automatically without changing menu settings
                },
                SubSystems = new[]
                {
                    Thrust, Utility, Offense, Power, Production, Any, // subsystems the gun targets
                },
                ClosestFirst = false, // tries to pick closest targets first (blocks on grids, projectiles, etc...).
                MinimumDiameter = 5, // 0 = unlimited, Minimum radius of threat to engage.
                MaximumDiameter = 0, // 0 = unlimited, Maximum radius of threat to engage.
                TopTargets = 4, // 0 = unlimited, max number of top targets to randomize between.
                TopBlocks = 4, // 0 = unlimited, max number of blocks to randomize between
				MaxTargetDistance = 800,
                StopTrackingSpeed = 1000, // do not track target threats traveling faster than this speed
            },
            HardPoint = new HardPointDef
            {
                WeaponName = "Glaive Heavy Turret", // name of weapon in terminal
                DeviateShotAngle = 0.85f,
                AimingTolerance = 1f, // 0 - 180 firing angle
                AimLeadingPrediction = Advanced, // Off, Basic, Accurate, Advanced
                DelayCeaseFire = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).

                Ui = new UiDef
                {
                    RateOfFire = true,
                    DamageModifier = false,
                    ToggleGuidance = false,
                    EnableOverload = false,
                },
                Ai = new AiDef
                {
                    TrackTargets = true,
                    TurretAttached = true,
                    TurretController = true,
                    PrimaryTracking = true,
                    LockOnFocus = false,
                },
                HardWare = new HardwareDef
                {
                    RotateRate = 0.3f,
                    ElevateRate = 0.3f,
                    MinAzimuth = -180,
                    MaxAzimuth = 180,
                    MinElevation = -10,
                    MaxElevation = 70,
                    FixedOffset = false,
                    InventorySize = 1f,
                    Offset = Vector(x: 0, y: 0, z:0),
                },
                Other = new OtherDef
                {
                    GridWeaponCap = 0,
                    RotateBarrelAxis = 0,
                    EnergyPriority = 0,
                    MuzzleCheck = false,
                    Debug = false,
                },
                Loading = new LoadingDef {
                    RateOfFire = 60,
                    BarrelSpinRate = 0, 
                    BarrelsPerShot = 1,
                    TrajectilesPerBarrel = 1, 
                    SkipBarrels = 0,
                    ReloadTime = 200, 
                    DelayUntilFire = 0, 
                    HeatPerShot = 0, 
                    MaxHeat = 0, 
                    Cooldown = 0, 
                    HeatSinkRate = 0, 
                    DegradeRof = false, 
                    ShotsInBurst = 6,
                    DelayAfterBurst = 0, 
                    FireFullBurst = false,
                    GiveUpAfterBurst = false,
                    DeterministicSpin = false, // Spin barrel position will always be relative to initial / starting positions (spin will not be as smooth).
                },
                Audio = new HardPointAudioDef {
                    PreFiringSound = "",
                    FiringSound = "NovaShot", // WepShipGatlingShot
                    FiringSoundPerShot = true,
                    ReloadSound = "LancerReload",
                    NoAmmoSound = "ArcWepShipGatlingNoAmmo",
                    HardPointRotationSound = "WepTurretGatlingRotate",
                    BarrelRotationSound = "WepShipGatlingRotation",
                    FireSoundEndDelay = 120, // Measured in game ticks(6 = 100ms, 60 = 1 seconds, etc..).
                },
                Graphics = new HardPointParticleDef {

					Barrel1 = new ParticleDef //FIXTHIS
                    {
                        Name = "AkiadMuzzleFlash", // Smoke_LargeGunShot
                        Color = Color(red: 1, green: 1, blue: 1, alpha: 1),
                        Offset = Vector(x: 0, y: 0, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = true,
                            MaxDistance = 500,
                            MaxDuration = 1,
                            Scale = 1f,
                        },
                    },
                },
            },
            Ammos = new [] {
                Nova88mmHEAT,HEShrapnel,
            },
             Animations= NovaTurret_Recoil
            //Upgrades = UpgradeModules,
            // Don't edit below this line
        };
		
		
		WeaponDefinition KrakenTurret45 => new WeaponDefinition {
            Assignments = new ModelAssignmentsDef
            {
                MountPoints = new[]
                {
                    new MountPointDef
                    {
                        SubtypeId = "KrakenTurret45",
                        AimPartId = "None",
                        MuzzlePartId = "MissileTurretBarrels",
                        AzimuthPartId = "MissileTurretBase1",
                        ElevationPartId = "MissileTurretBarrels",
                        DurabilityMod = 1f,
                        IconName = "None",
                    },
					new MountPointDef
                    {
                        SubtypeId = "KrakenTurret30",
                        AimPartId = "None",
                        MuzzlePartId = "MissileTurretBarrels",
                        AzimuthPartId = "MissileTurretBase1",
                        ElevationPartId = "MissileTurretBarrels",
                        DurabilityMod = 1f,
                        IconName = "None",
                    },
					new MountPointDef
                    {
                        SubtypeId = "LargeKrakenTurret45",
                        AimPartId = "None",
                        MuzzlePartId = "MissileTurretBarrels",
                        AzimuthPartId = "MissileTurretBase1",
                        ElevationPartId = "MissileTurretBarrels",
                        DurabilityMod = 1f,
                        IconName = "None",
                    },
					new MountPointDef
                    {
                        SubtypeId = "LargeKrakenTurret30",
                        AimPartId = "None",
                        MuzzlePartId = "MissileTurretBarrels",
                        AzimuthPartId = "MissileTurretBase1",
                        ElevationPartId = "MissileTurretBarrels",
                        DurabilityMod = 1f,
                        IconName = "None",
                    },

                },
                Barrels = new []
                {
					"muzzle_missile_1",
					"muzzle_missile_2",
					"muzzle_missile_3",
					"muzzle_missile_4",
					"muzzle_missile_5",
                },
            },
            Targeting = new TargetingDef
            {
                Threats = new[]
                {
                    Grids, Characters, Projectiles, Meteors, // threats percieved automatically without changing menu settings
                },
                SubSystems = new[]
                {
                    Thrust, Utility, Offense, Power, Production, Any, // subsystems the gun targets
                },
                ClosestFirst = false, // tries to pick closest targets first (blocks on grids, projectiles, etc...).
                MinimumDiameter = 5, // 0 = unlimited, Minimum radius of threat to engage.
                MaximumDiameter = 0, // 0 = unlimited, Maximum radius of threat to engage.
                TopTargets = 4, // 0 = unlimited, max number of top targets to randomize between.
                TopBlocks = 4, // 0 = unlimited, max number of blocks to randomize between
				MaxTargetDistance = 800,
                StopTrackingSpeed = 1000, // do not track target threats traveling faster than this speed
            },
            HardPoint = new HardPointDef
            {
                WeaponName = "Hydra ATGM (Turret)", // name of weapon in terminal
                DeviateShotAngle = 0.85f,
                AimingTolerance = 1f, // 0 - 180 firing angle
                AimLeadingPrediction = Advanced, // Off, Basic, Accurate, Advanced
                DelayCeaseFire = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).

                Ui = new UiDef
                {
                    RateOfFire = true,
                    DamageModifier = false,
                    ToggleGuidance = false,
                    EnableOverload = false,
                },
                Ai = new AiDef
                {
                    TrackTargets = true,
                    TurretAttached = true,
                    TurretController = true,
                    PrimaryTracking = true,
                    LockOnFocus = false,
                },
                HardWare = new HardwareDef
                {
                    RotateRate = 0.1f,
                    ElevateRate = 0.1f,
                    MinAzimuth = -45,
                    MaxAzimuth = 45,
                    MinElevation = -10,
                    MaxElevation = 45,
                    FixedOffset = false,
                    InventorySize = 3.24f,
                    Offset = Vector(x: 0, y: 0, z:0),
                },
                Other = new OtherDef
                {
                    GridWeaponCap = 0,
                    RotateBarrelAxis = 0,
                    EnergyPriority = 0,
                    MuzzleCheck = false,
                    Debug = false,
                },
                Loading = new LoadingDef {
                    RateOfFire = 80,
                    BarrelSpinRate = 0, 
                    BarrelsPerShot = 1,
                    TrajectilesPerBarrel = 1, 
                    SkipBarrels = 0,
                    ReloadTime = 300, 
                    DelayUntilFire = 30, 
                    HeatPerShot = 0, 
                    MaxHeat = 0, 
                    Cooldown = 0, 
                    HeatSinkRate = 0, 
                    DegradeRof = false, 
                    ShotsInBurst = 5,
                    DelayAfterBurst = 0, 
                    FireFullBurst = false,
                    GiveUpAfterBurst = false,
                    DeterministicSpin = false, // Spin barrel position will always be relative to initial / starting positions (spin will not be as smooth).
                },
                Audio = new HardPointAudioDef {
                    PreFiringSound = "",
                    FiringSound = "AresLaunch", // WepShipGatlingShot
                    FiringSoundPerShot = true,
                    ReloadSound = "AresReload",
                    NoAmmoSound = "ArcWepShipGatlingNoAmmo",
                    HardPointRotationSound = "WepTurretGatlingRotate",
                    BarrelRotationSound = "WepShipGatlingRotation",
                    FireSoundEndDelay = 120, // Measured in game ticks(6 = 100ms, 60 = 1 seconds, etc..).
                },
                Graphics = new HardPointParticleDef {

					Barrel1 = new ParticleDef //FIXTHIS
                    {
                        Name = "Smoke_LargeGunShot", // Smoke_LargeGunShot
                        Color = Color(red: 100, green: 60, blue: 10, alpha: 1),
                        Offset = Vector(x: 0, y: 0, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = true,
                            MaxDistance = 500,
                            MaxDuration = 1,
                            Scale = .3f,
                        },
                    },
                },
            },
            Ammos = new [] {
                Kraken800mm,KrakenStage2
            },
             Animations= Kraken_Launch
            //Upgrades = UpgradeModules,
            // Don't edit below this line
        };
		
		
		WeaponDefinition ThresherTurret45 => new WeaponDefinition {
            Assignments = new ModelAssignmentsDef
            {
                MountPoints = new[]
                {
                    new MountPointDef
                    {
                        SubtypeId = "ThresherTurret45",
                        AimPartId = "None",
                        MuzzlePartId = "MissileTurretBarrels",
                        AzimuthPartId = "MissileTurretBase1",
                        ElevationPartId = "MissileTurretBarrels",
                        DurabilityMod = 1f,
                        IconName = "None",
                    },
					new MountPointDef
                    {
                        SubtypeId = "ThresherTurret30",
                        AimPartId = "None",
                        MuzzlePartId = "MissileTurretBarrels",
                        AzimuthPartId = "MissileTurretBase1",
                        ElevationPartId = "MissileTurretBarrels",
                        DurabilityMod = 1f,
                        IconName = "None",
                    },
					new MountPointDef
                    {
                        SubtypeId = "LargeThresherTurret45",
                        AimPartId = "None",
                        MuzzlePartId = "MissileTurretBarrels",
                        AzimuthPartId = "MissileTurretBase1",
                        ElevationPartId = "MissileTurretBarrels",
                        DurabilityMod = 1f,
                        IconName = "None",
                    },
					new MountPointDef
                    {
                        SubtypeId = "LargeThresherTurret30",
                        AimPartId = "None",
                        MuzzlePartId = "MissileTurretBarrels",
                        AzimuthPartId = "MissileTurretBase1",
                        ElevationPartId = "MissileTurretBarrels",
                        DurabilityMod = 1f,
                        IconName = "None",
                    },

                },
                Barrels = new []
                {
					"muzzle_missile_001",
					"muzzle_missile_002",
					"muzzle_missile_003",
					"muzzle_missile_004",
                },
            },
            Targeting = new TargetingDef
            {
                Threats = new[]
                {
                    Grids, Characters, Projectiles, Meteors, // threats percieved automatically without changing menu settings
                },
                SubSystems = new[]
                {
                    Thrust, Utility, Offense, Power, Production, Any, // subsystems the gun targets
                },
                ClosestFirst = true, // tries to pick closest targets first (blocks on grids, projectiles, etc...).
                MinimumDiameter = 5, // 0 = unlimited, Minimum radius of threat to engage.
                MaximumDiameter = 0, // 0 = unlimited, Maximum radius of threat to engage.
                TopTargets = 4, // 0 = unlimited, max number of top targets to randomize between.
                TopBlocks = 4, // 0 = unlimited, max number of blocks to randomize between
				MaxTargetDistance = 1200,
                StopTrackingSpeed = 1000, // do not track target threats traveling faster than this speed
            },
            HardPoint = new HardPointDef
            {
                WeaponName = "Thresher Dual Turret", // name of weapon in terminal
                DeviateShotAngle = 0.85f,
                AimingTolerance = 1f, // 0 - 180 firing angle
                AimLeadingPrediction = Advanced, // Off, Basic, Accurate, Advanced
                DelayCeaseFire = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).

                Ui = new UiDef
                {
                    RateOfFire = true,
                    DamageModifier = false,
                    ToggleGuidance = false,
                    EnableOverload = false,
                },
                Ai = new AiDef
                {
                    TrackTargets = true,
                    TurretAttached = true,
                    TurretController = true,
                    PrimaryTracking = true,
                    LockOnFocus = true,
                },
                HardWare = new HardwareDef {
                    RotateRate = 0.3f,
                    ElevateRate = 0.3f,
                    MinAzimuth = -180,
                    MaxAzimuth = 180,
                    MinElevation = -15,
                    MaxElevation = 70,
                    FixedOffset = false,
                    InventorySize = 0.25f,
                    Offset = Vector(x: 0, y: 0, z:0),
                },
                Other = new OtherDef {
                    GridWeaponCap = 0,
                    RotateBarrelAxis = 0,
                    EnergyPriority = 0,
                    MuzzleCheck = false,
                    Debug = false,
                    RestrictionRadius = 0, // Meters, radius of sphere disable this gun if another is present
                    CheckInflatedBox = false, // if true, the bounding box of the gun is expanded by the RestrictionRadius
                    CheckForAnyWeapon = false, // if true, the check will fail if ANY gun is present, false only looks for this subtype
                },
                Loading = new LoadingDef {
                    RateOfFire = 100,
                    BarrelSpinRate = 0, 
                    BarrelsPerShot = 1,
                    TrajectilesPerBarrel = 1, 
                    SkipBarrels = 0,
                    ReloadTime = 0, 
                    DelayUntilFire = 40, 
                    HeatPerShot = 0, 
                    MaxHeat = 0, 
                    Cooldown = 0, 
                    HeatSinkRate = 0, 
                    DegradeRof = false, 
                    ShotsInBurst = 4,
                    DelayAfterBurst = 240, 
                    FireFullBurst = false,
                    GiveUpAfterBurst = false,
                    DeterministicSpin = false, // Spin barrel position will always be relative to initial / starting positions (spin will not be as smooth).
                },
                Audio = new HardPointAudioDef {
                    PreFiringSound = "Sigma11Prefire",
                    FiringSound = "ThresherFire", // WepShipGatlingShot
                    FiringSoundPerShot = true,
                    ReloadSound = "ThresherDoubleReload",
                    NoAmmoSound = "ArcWepShipGatlingNoAmmo",
                    HardPointRotationSound = "WepTurretGatlingRotate",
                    BarrelRotationSound = "WepShipGatlingRotation",
                    FireSoundEndDelay = 120, // Measured in game ticks(6 = 100ms, 60 = 1 seconds, etc..).
                },
                Graphics = new HardPointParticleDef {

					Barrel1 = new ParticleDef {
                        Name = "AkiadMuzzle", // Smoke_LargeGunShot
                        Color = Color(red: 1, green: 1, blue: 1, alpha: 1),
                        Offset = Vector(x: 0, y: 0, z: 0),

                        Extras = new ParticleOptionDef {
                            Loop = true,
                            Restart = true,
                            MaxDistance = 5000,
                            MaxDuration = 10,
                            Scale = 1f,
                        },
                    },
                },
            },
            Ammos = new [] {
                ThresherMissile,HEShrapnel,SmartBomb,ThresherMIRV
            },
            Animations= ThresherTurret_Launch,
            //Upgrades = UpgradeModules,
            // Don't edit below this line
        };
		
		
		WeaponDefinition PlasmaRepeaterTurret45 => new WeaponDefinition {
            Assignments = new ModelAssignmentsDef
            {
                MountPoints = new[]
                {
                    new MountPointDef
                    {
                        SubtypeId = "PlasmaRepeaterTurret45",
                        AimPartId = "None",
                        MuzzlePartId = "MissileTurretBarrels",
                        AzimuthPartId = "MissileTurretBase1",
                        ElevationPartId = "MissileTurretBarrels",
                        DurabilityMod = 1f,
                        IconName = "None",
                    },
					new MountPointDef
                    {
                        SubtypeId = "PlasmaRepeaterTurret30",
                        AimPartId = "None",
                        MuzzlePartId = "MissileTurretBarrels",
                        AzimuthPartId = "MissileTurretBase1",
                        ElevationPartId = "MissileTurretBarrels",
                        DurabilityMod = 1f,
                        IconName = "None",
                    },
					new MountPointDef
                    {
                        SubtypeId = "LargePlasmaRepeaterTurret45",
                        AimPartId = "None",
                        MuzzlePartId = "MissileTurretBarrels",
                        AzimuthPartId = "MissileTurretBase1",
                        ElevationPartId = "MissileTurretBarrels",
                        DurabilityMod = 1f,
                        IconName = "None",
                    },
					new MountPointDef
                    {
                        SubtypeId = "LargePlasmaRepeaterTurret30",
                        AimPartId = "None",
                        MuzzlePartId = "MissileTurretBarrels",
                        AzimuthPartId = "MissileTurretBase1",
                        ElevationPartId = "MissileTurretBarrels",
                        DurabilityMod = 1f,
                        IconName = "None",
                    },

                },
                Barrels = new []
                {
					"muzzle_missile_001",
					"muzzle_missile_002",
                },
            },
            Targeting = new TargetingDef
            {
                Threats = new[]
                {
                    Grids, Characters, Projectiles, Meteors, // threats percieved automatically without changing menu settings
                },
                SubSystems = new[]
                {
                    Thrust, Utility, Offense, Power, Production, Any, // subsystems the gun targets
                },
                ClosestFirst = false, // tries to pick closest targets first (blocks on grids, projectiles, etc...).
                MinimumDiameter = 5, // 0 = unlimited, Minimum radius of threat to engage.
                MaximumDiameter = 0, // 0 = unlimited, Maximum radius of threat to engage.
                TopTargets = 4, // 0 = unlimited, max number of top targets to randomize between.
                TopBlocks = 4, // 0 = unlimited, max number of blocks to randomize between
				MaxTargetDistance = 800,
                StopTrackingSpeed = 1000, // do not track target threats traveling faster than this speed
            },
            HardPoint = new HardPointDef
            {
                WeaponName = "Arbalest Plasma Repeater Turret", // name of weapon in terminal
                DeviateShotAngle = 0.85f,
                AimingTolerance = 1f, // 0 - 180 firing angle
                AimLeadingPrediction = Advanced, // Off, Basic, Accurate, Advanced
                DelayCeaseFire = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).

                Ui = new UiDef
                {
                    RateOfFire = true,
                    DamageModifier = false,
                    ToggleGuidance = false,
                    EnableOverload = false,
                },
                Ai = new AiDef
                {
                    TrackTargets = true,
                    TurretAttached = true,
                    TurretController = true,
                    PrimaryTracking = true,
                    LockOnFocus = false,
                },
                HardWare = new HardwareDef
                {
                    RotateRate = 0.3f,
                    ElevateRate = 0.3f,
                    MinAzimuth = -180,
                    MaxAzimuth = 180,
                    MinElevation = -10,
                    MaxElevation = 80,
                    FixedOffset = false,
                    InventorySize = 3.24f,
                    Offset = Vector(x: 0, y: 0, z:0),
                },
                Other = new OtherDef
                {
                    GridWeaponCap = 0,
                    RotateBarrelAxis = 0,
                    EnergyPriority = 0,
                    MuzzleCheck = false,
                    Debug = false,
                },
                Loading = new LoadingDef {
                    RateOfFire = 400,
                    BarrelSpinRate = 0, // visual only, 0 disables and uses RateOfFire
                    BarrelsPerShot = 1,
                    TrajectilesPerBarrel = 1, // Number of Trajectiles per barrel per fire event.
                    SkipBarrels = 0,
                    ReloadTime = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    DelayUntilFire = 30, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    HeatPerShot = 60, //heat generated per shot
                    MaxHeat = 2000, //max heat before weapon enters cooldown (70% of max heat)
                    Cooldown = .16667f, //percent of max heat to be under to start firing again after overheat accepts .2-.95
                    HeatSinkRate = 100, //amount of heat lost per second
                    DegradeRof = false, // progressively lower rate of fire after 80% heat threshold (80% of max heat)
                    ShotsInBurst = 0,
                    DelayAfterBurst = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    FireFullBurst = false, // Spin barrel position will always be relative to initial / starting positions (spin will not be as smooth).
                },
                Audio = new HardPointAudioDef {
                    PreFiringSound = "",
                    FiringSound = "RepeaterShot", // WepShipGatlingShot
                    FiringSoundPerShot = true,
                    ReloadSound = "",
                    NoAmmoSound = "ArcWepShipGatlingNoAmmo",
                    HardPointRotationSound = "WepTurretGatlingRotate",
                    BarrelRotationSound = "WepShipGatlingRotation",
                    FireSoundEndDelay = 120, // Measured in game ticks(6 = 100ms, 60 = 1 seconds, etc..).
                },
                Graphics = new HardPointParticleDef {

					Barrel1 = new ParticleDef //FIXTHIS
                    {
                        Name = "TestMuzzle", // Smoke_LargeGunShot
                        Color = Color(red: 0, green: 10, blue: 200, alpha: 1),
                        Offset = Vector(x: 0, y: 0, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = true,
                            MaxDistance = 500,
                            MaxDuration = 1,
                            Scale = .3f,
                        },
                    },
                },
            },
            Ammos = new [] {
                RepeaterBolt,
            },
             Animations= Arbalest_Emissive
            //Upgrades = UpgradeModules,
            // Don't edit below this line
        };
		
		
		WeaponDefinition ReceptorTurret45 => new WeaponDefinition {
            Assignments = new ModelAssignmentsDef
            {
                MountPoints = new[]
                {
                    new MountPointDef
                    {
                        SubtypeId = "ReceptorTurret45",
                        AimPartId = "None",
                        MuzzlePartId = "MissileTurretBarrels",
                        AzimuthPartId = "MissileTurretBase1",
                        ElevationPartId = "MissileTurretBarrels",
                        DurabilityMod = 1f,
                        IconName = "None",
                    },
					new MountPointDef
                    {
                        SubtypeId = "ReceptorTurret30",
                        AimPartId = "None",
                        MuzzlePartId = "MissileTurretBarrels",
                        AzimuthPartId = "MissileTurretBase1",
                        ElevationPartId = "MissileTurretBarrels",
                        DurabilityMod = 1f,
                        IconName = "None",
                    },
					new MountPointDef
                    {
                        SubtypeId = "LargeReceptorTurret45",
                        AimPartId = "None",
                        MuzzlePartId = "MissileTurretBarrels",
                        AzimuthPartId = "MissileTurretBase1",
                        ElevationPartId = "MissileTurretBarrels",
                        DurabilityMod = 1f,
                        IconName = "None",
                    },
					new MountPointDef
                    {
                        SubtypeId = "LargeReceptorTurret30",
                        AimPartId = "None",
                        MuzzlePartId = "MissileTurretBarrels",
                        AzimuthPartId = "MissileTurretBase1",
                        ElevationPartId = "MissileTurretBarrels",
                        DurabilityMod = 1f,
                        IconName = "None",
                    },
					

                },
                Barrels = new []
                {
					"muzzle_missile_001",
                },
            },
            Targeting = new TargetingDef
            {
                Threats = new[]
                {
                    Grids, Characters, Projectiles, Meteors, // threats percieved automatically without changing menu settings
                },
                SubSystems = new[]
                {
                    Thrust, Utility, Offense, Power, Production, Any, // subsystems the gun targets
                },
                ClosestFirst = false, // tries to pick closest targets first (blocks on grids, projectiles, etc...).
                MinimumDiameter = 5, // 0 = unlimited, Minimum radius of threat to engage.
                MaximumDiameter = 0, // 0 = unlimited, Maximum radius of threat to engage.
                TopTargets = 4, // 0 = unlimited, max number of top targets to randomize between.
                TopBlocks = 4, // 0 = unlimited, max number of blocks to randomize between
				MaxTargetDistance = 1200,
                StopTrackingSpeed = 1000, // do not track target threats traveling faster than this speed
            },
            HardPoint = new HardPointDef
            {
                WeaponName = "Receptor Plasma Launcher Turret", // name of weapon in terminal
                DeviateShotAngle = 0.85f,
                AimingTolerance = 1f, // 0 - 180 firing angle
                AimLeadingPrediction = Advanced, // Off, Basic, Accurate, Advanced
                DelayCeaseFire = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).

                Ui = new UiDef
                {
                    RateOfFire = true,
                    DamageModifier = false,
                    ToggleGuidance = false,
                    EnableOverload = false,
                },
                Ai = new AiDef
                {
                    TrackTargets = true,
                    TurretAttached = true,
                    TurretController = true,
                    PrimaryTracking = true,
                    LockOnFocus = false,
                },
                HardWare = new HardwareDef
                {
                    RotateRate = 0.3f,
                    ElevateRate = 0.3f,
                    MinAzimuth = -180,
                    MaxAzimuth = 180,
                    MinElevation = -10,
                    MaxElevation = 80,
                    FixedOffset = false,
                    InventorySize = 3.24f,
                    Offset = Vector(x: 0, y: 0, z:0),
                },
                Other = new OtherDef
                {
                    GridWeaponCap = 0,
                    RotateBarrelAxis = 0,
                    EnergyPriority = 0,
                    MuzzleCheck = false,
                    Debug = false,
                },
                Loading = new LoadingDef {
                    RateOfFire = 100,
                    BarrelSpinRate = 0, // visual only, 0 disables and uses RateOfFire
                    BarrelsPerShot = 1,
                    TrajectilesPerBarrel = 1, // Number of Trajectiles per barrel per fire event.
                    SkipBarrels = 0,
                    ReloadTime = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    DelayUntilFire = 30, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    HeatPerShot = 600, //heat generated per shot
                    MaxHeat = 1500, //max heat before weapon enters cooldown (70% of max heat)
                    Cooldown = .16667f, //percent of max heat to be under to start firing again after overheat accepts .2-.95
                    HeatSinkRate = 200, //amount of heat lost per second
                    DegradeRof = false, // progressively lower rate of fire after 80% heat threshold (80% of max heat)
                    ShotsInBurst = 0,
                    DelayAfterBurst = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    FireFullBurst = false, // Spin barrel position will always be relative to initial / starting positions (spin will not be as smooth).
                },
                Audio = new HardPointAudioDef {
                    PreFiringSound = "CoilGunPrefire",
                    FiringSound = "CoilGunFire", // WepShipGatlingShot
                    FiringSoundPerShot = true,
                    ReloadSound = "",
                    NoAmmoSound = "ArcWepShipGatlingNoAmmo",
                    HardPointRotationSound = "WepTurretGatlingRotate",
                    BarrelRotationSound = "WepShipGatlingRotation",
                    FireSoundEndDelay = 120, // Measured in game ticks(6 = 100ms, 60 = 1 seconds, etc..).
                },
                Graphics = new HardPointParticleDef {

					Barrel1 = new ParticleDef //FIXTHIS
                    {
                        Name = "TestMuzzle", // Smoke_LargeGunShot
                        Color = Color(red: 1, green: 200, blue: 100, alpha: 1),
                        Offset = Vector(x: 0, y: 0, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = true,
                            MaxDistance = 500,
                            MaxDuration = 1,
                            Scale = 5f,
                        },
                    },
                },
            },
            Ammos = new [] {
                Plasma,
            },
             Animations= Receptor_Emissive
            //Upgrades = UpgradeModules,
            // Don't edit below this line
        };
		












	
    }
}