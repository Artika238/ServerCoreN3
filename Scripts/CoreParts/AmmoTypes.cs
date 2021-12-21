using static Scripts.Structure.WeaponDefinition;
using static Scripts.Structure.WeaponDefinition.AmmoDef;
using static Scripts.Structure.WeaponDefinition.AmmoDef.EjectionDef;
using static Scripts.Structure.WeaponDefinition.AmmoDef.EjectionDef.SpawnType;
using static Scripts.Structure.WeaponDefinition.AmmoDef.ShapeDef.Shapes;
using static Scripts.Structure.WeaponDefinition.AmmoDef.GraphicDef;
using static Scripts.Structure.WeaponDefinition.AmmoDef.TrajectoryDef;
using static Scripts.Structure.WeaponDefinition.AmmoDef.TrajectoryDef.GuidanceType;
using static Scripts.Structure.WeaponDefinition.AmmoDef.DamageScaleDef;
using static Scripts.Structure.WeaponDefinition.AmmoDef.DamageScaleDef.ShieldDef.ShieldType;
using static Scripts.Structure.WeaponDefinition.AmmoDef.AreaDamageDef;
using static Scripts.Structure.WeaponDefinition.AmmoDef.AreaDamageDef.AreaEffectType;
using static Scripts.Structure.WeaponDefinition.AmmoDef.AreaDamageDef.EwarFieldsDef;
using static Scripts.Structure.WeaponDefinition.AmmoDef.AreaDamageDef.EwarFieldsDef.PushPullDef.Force;
using static Scripts.Structure.WeaponDefinition.AmmoDef.GraphicDef.LineDef;
using static Scripts.Structure.WeaponDefinition.AmmoDef.GraphicDef.LineDef.TracerBaseDef;
using static Scripts.Structure.WeaponDefinition.AmmoDef.GraphicDef.LineDef.Texture;
using static Scripts.Structure.WeaponDefinition.AmmoDef.DamageScaleDef.DamageTypes.Damage;

namespace Scripts
{ // Don't edit above this line
    partial class Parts
    {
        private AmmoDef Lancer50standard => new AmmoDef
        {
            AmmoMagazine = "Lancer50standard",
            AmmoRound = "50mm Standard",
            HybridRound = false, //AmmoMagazine based weapon with energy cost
            EnergyCost = 0.00000000001f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
            BaseDamage = 1600f,
            Mass = 13f, // in kilograms
            Health = 0, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying.
            BackKickForce = 1000f,
            DecayPerShot = 0f,
            HardPointUsable = true, // set to false if this is a Fragment ammoType and you don't want the turret to be able to select it directly.
            EnergyMagazineSize = 12, //ONLY USE IF "AmmoMagazine=ENERGY". Otherwise specify magazine size in Ammo.sbc file.
            IgnoreWater = false,

            Shape = new ShapeDef //defines the collision shape of projectile, defaults line and visual Line Length if set to 0
            {
                Shape = LineShape, // LineShape or SphereShape. Do not use SphereShape for fast moving projectiles if you care about precision.
                Diameter = 1, // Diameter is minimum length of LineShape or minimum diameter of SphereShape
            },
            ObjectsHit = new ObjectsHitDef
            {
                MaxObjectsHit = 0, // 0 = disabled
                CountBlocks = false, // counts gridBlocks and not just entities hit
            },
            Fragment = new FragmentDef
            {
                AmmoRound = "",
                Fragments = 0,
                Degrees = 15,
                Reverse = false,
                RandomizeDir = false, // randomize between forward and backward directions
            },
            Pattern = new PatternDef
            {
                Patterns = new[] {
                    "",
                },
                Enable = false,
                TriggerChance = 1f,
                Random = false,
                RandomMin = 1,
                RandomMax = 1,
                SkipParent = false,
                PatternSteps = 1, // Number of Patterns activated per round, will progress in order and loop.  Ignored if Random = true.
            },
            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0f, // 0 = disabled, 1000 = any blocks with currently integrity above 1000 will be immune to damage.
                DamageVoxels = false, // true = voxels are vulnerable to this weapon
                SelfDamage = false, // true = allow self damage.
                HealthHitModifier = 2, // defaults to a value of 1, this setting modifies how much Health is subtracted from a projectile per hit (1 = per hit).
                VoxelHitModifier = 10,
                Characters = 1f,
                // modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01 = 1% damage, 2 = 200% damage.
                FallOff = new FallOffDef
                {
                    Distance = 1000f, // Distance at which max damage begins falling off.
                    MinMultipler = 0f, // value from 0.0f to 1f where 0.1f would be a min damage of 10% of max damage.
                },
                Grids = new GridSizeDef
                {
                    Large = 1f,
                    Small = .6f,
                },
                Armor = new ArmorDef
                {
                    Armor = 1f,
                    Light = -1f,
                    Heavy = 1f,
                    NonArmor = 1f,
                },
                Shields = new ShieldDef
                {
                    Modifier = 1f,
                    Type = Default,
                    BypassModifier = -1f,
                },
                // first true/false (ignoreOthers) will cause projectiles to pass through all blocks that do not match the custom subtypeIds.
                Custom = new CustomScalesDef
                {
                    IgnoreAllOthers = false,
                    Types = new[]
                    {
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test1",
                            Modifier = -1f,
                        },
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test2",
                            Modifier = -1f,
                        },
                    },
                },
            },
            AreaEffect = new AreaDamageDef
            {
                AreaEffect = Disabled, // Disabled = do not use area effect at all, Explosive, Radiant, AntiSmart, JumpNullField, JumpNullField, EnergySinkField, AnchorField, EmpField, OffenseField, NavField, DotField.
                Base = new AreaInfluence
                {
                    Radius = 10f, // the sphere of influence of area effects
                    EffectStrength = 0f, // For ewar it applies this amount per pulse/hit, non-ewar applies this as damage per tick per entity in area of influence. For radiant 0 == use spillover from BaseDamage, otherwise use this value.
                },
                Pulse = new PulseDef // interval measured in game ticks (60 == 1 second), pulseChance chance (0 - 100) that an entity in field will be hit
                {
                    Interval = 0,
                    PulseChance = 0,
                    GrowTime = 0,
                    HideModel = false,
                    ShowParticle = false,
                    Particle = new ParticleDef
                    {
                        Name = "", //ShipWelderArc
                        ShrinkByDistance = false,
                        Color = Color(red: 128, green: 0, blue: 0, alpha: 32),
                        Offset = Vector(x: 0, y: -1, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 1,
                            Scale = 1,
                        },
                    },
                },
                Explosions = new ExplosionDef
                {
                    NoVisuals = false,
                    NoSound = false,
                    NoShrapnel = false,
                    NoDeformation = true,
                    Scale = 1,
                    CustomParticle = "",
                    CustomSound = "",
                },
                Detonation = new DetonateDef
                {
                    DetonateOnEnd = false,
                    ArmOnlyOnHit = false,
                    DetonationDamage = 0,
                    DetonationRadius = 0,
                    MinArmingTime = 0, //Min time in ticks before projectile will arm for detonation (will also affect Fragment spawning)
                },
                EwarFields = new EwarFieldsDef
                {
                    Duration = 1,
                    StackDuration = false,
                    Depletable = false,
                    MaxStacks = 0,
                    TriggerRange = 0f,
                    DisableParticleEffect = true,
                    Force = new PushPullDef // AreaEffectDamage is multiplied by target mass.
                    {
                        ForceFrom = ProjectileLastPosition, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
                        ForceTo = HitPosition, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
                        Position = TargetCenterOfMass, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
                    },
                },
            },
            Beams = new BeamDef
            {
                Enable = false,
                VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
                ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
                RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
                OneParticle = false, // Only spawn one particle hit per beam weapon.
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = None,
                TargetLossDegree = 80f,
                TargetLossTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                MaxLifeTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                AccelPerSec = 0f,
                DesiredSpeed = 400, // DO NOT SET HIGHER THAN 4100
                MaxTrajectory = 5000f,
                FieldTime = 0, // 0 is disabled, a value causes the projectile to come to rest, spawn a field and remain for a time (Measured in game ticks, 60 = 1 second)
                GravityMultiplier = 0f, // Gravity multiplier, influences the trajectory of the projectile, value greater than 0 to enable.
                SpeedVariance = Random(start: 0, end: 0), // subtracts value from DesiredSpeed
                RangeVariance = Random(start: 0, end: 0), // subtracts value from MaxTrajectory
                MaxTrajectoryTime = 0, // How long the weapon must fire before it reaches MaxTrajectory.
                Smarts = new SmartsDef
                {
                    Inaccuracy = 0f, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
                    Aggressiveness = 1f, // controls how responsive tracking is.
                    MaxLateralThrust = 0.5, // controls how sharp the trajectile may turn
                    TrackingDelay = 0, // Measured in Shape diameter units traveled.
                    MaxChaseTime = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    OverideTarget = true, // when set to true ammo picks its own target, does not use hardpoint's.
                    MaxTargets = 0, // Number of targets allowed before ending, 0 = unlimited
                    NoTargetExpire = false, // Expire without ever having a target at TargetLossTime
                    Roam = false, // Roam current area after target loss
                    KeepAliveAfterTargetLoss = false, // Whether to stop early death of projectile on target loss
                },
                Mines = new MinesDef
                {
                    DetectRadius = 0,
                    DeCloakRadius = 0,
                    FieldTime = 0,
                    Cloak = false,
                    Persist = false,
                },
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "",
                VisualProbability = 1f,
                ShieldHitDraw = false,
                Particles = new AmmoParticleDef
                {
                    Ammo = new ParticleDef
                    {
                        Name = "", //ShipWelderArc
                        ShrinkByDistance = false,
                        Color = Color(red: 128, green: 0, blue: 0, alpha: 32),
                        Offset = Vector(x: 0, y: -1, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 1,
                            Scale = 1,
                        },
                    },
                    Hit = new ParticleDef
                    {
                        Name = "",
                        ApplyToShield = true,
                        ShrinkByDistance = false,
                        Color = Color(red: 3, green: 1.9f, blue: 1f, alpha: 1),
                        Offset = Vector(x: 0, y: 0, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 30,
                            Scale = 1,
                            HitPlayChance = 1f,
                        },
                    },
                    Eject = new ParticleDef
                    {
                        Name = "",
                        ApplyToShield = true,
                        ShrinkByDistance = false,
                        Color = Color(red: 3, green: 1.9f, blue: 1f, alpha: 1),
                        Offset = Vector(x: 0, y: 0, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 30,
                            Scale = 1,
                            HitPlayChance = 1f,
                        },
                    },
                },
                Lines = new LineDef
                {
                    ColorVariance = Random(start: 0.75f, end: 2f), // multiply the color by random values within range.
                    WidthVariance = Random(start: 0f, end: 0f), // adds random value to default width (negatives shrinks width)
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = 1f,
                        Width = 0.2f,
                        Color = Color(red: 3, green: 2, blue: 1f, alpha: 1),
                        VisualFadeStart = 0, // Number of ticks the weapon has been firing before projectiles begin to fade their color
                        VisualFadeEnd = 0, // How many ticks after fade began before it will be invisible.
                        Textures = new[] {// WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                            "WeaponLaser",
                        },
                        TextureMode = Normal, // Normal, Cycle, Chaos, Wave
                        Segmentation = new SegmentDef
                        {
                            Enable = false, // If true Tracer TextureMode is ignored
                            Textures = new[] {
								"",
                            },
                            SegmentLength = 0f, // Uses the values below.
                            SegmentGap = 0f, // Uses Tracer textures and values
                            Speed = 1f, // meters per second
                            Color = Color(red: 1, green: 2, blue: 2.5f, alpha: 1),
                            WidthMultiplier = 1f,
                            Reverse = false,
                            UseLineVariance = true,
                            WidthVariance = Random(start: 0f, end: 0f),
                            ColorVariance = Random(start: 0f, end: 0f)
                        }
                    },
                    Trail = new TrailDef
                    {
                        Enable = false,
                        Textures = new[] {
							"",
                        },
                        TextureMode = Normal,
                        DecayTime = 128,
                        Color = Color(red: 0, green: 0, blue: 1, alpha: 1),
                        Back = false,
                        CustomWidth = 0,
                        UseWidthVariance = false,
                        UseColorFade = true,
                    },
                    OffsetEffect = new OffsetEffectDef
                    {
                        MaxOffset = 0,// 0 offset value disables this effect
                        MinLength = 0.2f,
                        MaxLength = 3,
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                TravelSound = "",
                HitSound = "",
                ShieldHitSound = "",
                PlayerHitSound = "",
                VoxelHitSound = "",
                FloatingHitSound = "",
                HitPlayChance = 0.5f,
                HitPlayShield = true,
            }, // Don't edit below this line
            Ejection = new EjectionDef
            {
                Type = Particle, // Particle or Item (Inventory Component)
                Speed = 100f, // Speed inventory is ejected from in dummy direction
                SpawnChance = 0.5f, // chance of triggering effect (0 - 1)
                CompDef = new ComponentDef
                {
                    ItemName = "", //InventoryComponent name
                    ItemLifeTime = 0, // how long item should exist in world
                    Delay = 0, // delay in ticks after shot before ejected
                }
            },
        };
		
		private AmmoDef Flak => new AmmoDef
        {
            AmmoMagazine = "FlakBelt",
            AmmoRound = "600m",
            HybridRound = false, //AmmoMagazine based weapon with energy cost
            EnergyCost = 0.00000000001f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
            BaseDamage = 200f,
            Mass = 12f, // in kilograms
            Health = 0, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying.
            BackKickForce = 1000f,
            DecayPerShot = 0f,
            HardPointUsable = true, // set to false if this is a Fragment ammoType and you don't want the turret to be able to select it directly.
            EnergyMagazineSize = 12, //ONLY USE IF "AmmoMagazine=ENERGY". Otherwise specify magazine size in Ammo.sbc file.
            IgnoreWater = false,

            Shape = new ShapeDef //defines the collision shape of projectile, defaults line and visual Line Length if set to 0
            {
                Shape = LineShape, // LineShape or SphereShape. Do not use SphereShape for fast moving projectiles if you care about precision.
                Diameter = 1, // Diameter is minimum length of LineShape or minimum diameter of SphereShape
            },
            ObjectsHit = new ObjectsHitDef
            {
                MaxObjectsHit = 0, // 0 = disabled
                CountBlocks = false, // counts gridBlocks and not just entities hit
            },
            Fragment = new FragmentDef
            {
                AmmoRound = "FlakShrapnel",
                Fragments = 20,
                Degrees = 200,
                Reverse = false,
                RandomizeDir = false, // randomize between forward and backward directions
            },
            Pattern = new PatternDef
            {
                Patterns = new[] {
                    "",
                },
                Enable = false,
                TriggerChance = 1f,
                Random = false,
                RandomMin = 1,
                RandomMax = 1,
                SkipParent = false,
                PatternSteps = 1, // Number of Patterns activated per round, will progress in order and loop.  Ignored if Random = true.
            },
            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0f, // 0 = disabled, 1000 = any blocks with currently integrity above 1000 will be immune to damage.
                DamageVoxels = false, // true = voxels are vulnerable to this weapon
                SelfDamage = false, // true = allow self damage.
                HealthHitModifier = 1.5, // defaults to a value of 1, this setting modifies how much Health is subtracted from a projectile per hit (1 = per hit).
                VoxelHitModifier = 10,
                Characters = 1f,
                // modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01 = 1% damage, 2 = 200% damage.
                FallOff = new FallOffDef
                {
                    Distance = 1000f, // Distance at which max damage begins falling off.
                    MinMultipler = 0f, // value from 0.0f to 1f where 0.1f would be a min damage of 10% of max damage.
                },
                Grids = new GridSizeDef
                {
                    Large = 1f,
                    Small = .6f,
                },
                Armor = new ArmorDef
                {
                    Armor = 1f,
                    Light = -1f,
                    Heavy = 1f,
                    NonArmor = -1f,
                },
                Shields = new ShieldDef
                {
                    Modifier = 1f,
                    Type = Default,
                    BypassModifier = -1f,
                },
				DamageType = new DamageTypes // Damage type of each element of the projectile's damage; Kinetic, Energy
                {
                    Base = Kinetic,
                    AreaEffect = Energy,
                    Detonation = Energy,
                    Shield = Energy, // Damage against shields is currently all of one type per projectile.
                },
                // first true/false (ignoreOthers) will cause projectiles to pass through all blocks that do not match the custom subtypeIds.
                Custom = new CustomScalesDef
                {
                    IgnoreAllOthers = false,
                    Types = new[]
                    {
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test1",
                            Modifier = -1f,
                        },
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test2",
                            Modifier = -1f,
                        },
                    },
                },
            },
            AreaEffect = new AreaDamageDef
            {
                AreaEffect = Explosive, // Disabled = do not use area effect at all, Explosive, Radiant, AntiSmart, JumpNullField, JumpNullField, EnergySinkField, AnchorField, EmpField, OffenseField, NavField, DotField.
                Base = new AreaInfluence
                {
                    Radius = 10f, // the sphere of influence of area effects
                    EffectStrength = 0f, // For ewar it applies this amount per pulse/hit, non-ewar applies this as damage per tick per entity in area of influence. For radiant 0 == use spillover from BaseDamage, otherwise use this value.
                },
                Pulse = new PulseDef // interval measured in game ticks (60 == 1 second), pulseChance chance (0 - 100) that an entity in field will be hit
                {
                    Interval = 0,
                    PulseChance = 0,
                    GrowTime = 0,
                    HideModel = false,
                    ShowParticle = false,
                    Particle = new ParticleDef
                    {
                        Name = "", //ShipWelderArc
                        ShrinkByDistance = false,
                        Color = Color(red: 128, green: 0, blue: 0, alpha: 32),
                        Offset = Vector(x: 0, y: -1, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 1,
                            Scale = 1,
                        },
                    },
                },
                Explosions = new ExplosionDef
                {
                    NoVisuals = false,
                    NoSound = false,
                    NoShrapnel = false,
                    NoDeformation = true,
                    Scale = 1,
                    CustomParticle = "",
                    CustomSound = "",
                },
                Detonation = new DetonateDef
                {
                    DetonateOnEnd = true,
                    ArmOnlyOnHit = false,
                    DetonationDamage = 60,
                    DetonationRadius = 1,
                    MinArmingTime = 0, //Min time in ticks before projectile will arm for detonation (will also affect Fragment spawning)
                },
                EwarFields = new EwarFieldsDef
                {
                    Duration = 1,
                    StackDuration = false,
                    Depletable = false,
                    MaxStacks = 0,
                    TriggerRange = 0f,
                    DisableParticleEffect = true,
                    Force = new PushPullDef // AreaEffectDamage is multiplied by target mass.
                    {
                        ForceFrom = ProjectileLastPosition, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
                        ForceTo = HitPosition, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
                        Position = TargetCenterOfMass, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
                    },
                },
            },
            Beams = new BeamDef
            {
                Enable = false,
                VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
                ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
                RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
                OneParticle = false, // Only spawn one particle hit per beam weapon.
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = None,
                TargetLossDegree = 80f,
                TargetLossTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                MaxLifeTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                AccelPerSec = 0f,
                DesiredSpeed = 400, // DO NOT SET HIGHER THAN 4100
                MaxTrajectory = 5000f,
                FieldTime = 0, // 0 is disabled, a value causes the projectile to come to rest, spawn a field and remain for a time (Measured in game ticks, 60 = 1 second)
                GravityMultiplier = 0f, // Gravity multiplier, influences the trajectory of the projectile, value greater than 0 to enable.
                SpeedVariance = Random(start: 0, end: 0), // subtracts value from DesiredSpeed
                RangeVariance = Random(start: 0, end: 0), // subtracts value from MaxTrajectory
                MaxTrajectoryTime = 0, // How long the weapon must fire before it reaches MaxTrajectory.
                Smarts = new SmartsDef
                {
                    Inaccuracy = 0f, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
                    Aggressiveness = 1f, // controls how responsive tracking is.
                    MaxLateralThrust = 0.5, // controls how sharp the trajectile may turn
                    TrackingDelay = 0, // Measured in Shape diameter units traveled.
                    MaxChaseTime = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    OverideTarget = true, // when set to true ammo picks its own target, does not use hardpoint's.
                    MaxTargets = 0, // Number of targets allowed before ending, 0 = unlimited
                    NoTargetExpire = false, // Expire without ever having a target at TargetLossTime
                    Roam = false, // Roam current area after target loss
                    KeepAliveAfterTargetLoss = false, // Whether to stop early death of projectile on target loss
                },
                Mines = new MinesDef
                {
                    DetectRadius = 0,
                    DeCloakRadius = 0,
                    FieldTime = 0,
                    Cloak = false,
                    Persist = false,
                },
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "\\Models\\Akiad\\Small\\FlakRound.mwm",
                VisualProbability = 1f,
                ShieldHitDraw = false,
                Particles = new AmmoParticleDef
                {
                    Ammo = new ParticleDef
                    {
                        Name = "", //ShipWelderArc
                        ShrinkByDistance = false,
                        Color = Color(red: 128, green: 0, blue: 0, alpha: 32),
                        Offset = Vector(x: 0, y: -1, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 1,
                            Scale = 1,
                        },
                    },
                    Hit = new ParticleDef
                    {
                        Name = "",
                        ApplyToShield = true,
                        ShrinkByDistance = false,
                        Color = Color(red: 3, green: 1.9f, blue: 1f, alpha: 1),
                        Offset = Vector(x: 0, y: 0, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 30,
                            Scale = 1,
                            HitPlayChance = 1f,
                        },
                    },
                    Eject = new ParticleDef
                    {
                        Name = "",
                        ApplyToShield = true,
                        ShrinkByDistance = false,
                        Color = Color(red: 3, green: 1.9f, blue: 1f, alpha: 1),
                        Offset = Vector(x: 0, y: 0, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 30,
                            Scale = 1,
                            HitPlayChance = 1f,
                        },
                    },
                },
                Lines = new LineDef
                {
                    ColorVariance = Random(start: 0.75f, end: 2f), // multiply the color by random values within range.
                    WidthVariance = Random(start: 0f, end: 0f), // adds random value to default width (negatives shrinks width)
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = 1f,
                        Width = 0.2f,
                        Color = Color(red: 3, green: 2, blue: 1f, alpha: 1),
                        VisualFadeStart = 0, // Number of ticks the weapon has been firing before projectiles begin to fade their color
                        VisualFadeEnd = 0, // How many ticks after fade began before it will be invisible.
                        Textures = new[] {// WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                            "WeaponLaser",
                        },
                        TextureMode = Normal, // Normal, Cycle, Chaos, Wave
                        Segmentation = new SegmentDef
                        {
                            Enable = false, // If true Tracer TextureMode is ignored
                            Textures = new[] {
								"",
                            },
                            SegmentLength = 0f, // Uses the values below.
                            SegmentGap = 0f, // Uses Tracer textures and values
                            Speed = 1f, // meters per second
                            Color = Color(red: 1, green: 2, blue: 2.5f, alpha: 1),
                            WidthMultiplier = 1f,
                            Reverse = false,
                            UseLineVariance = true,
                            WidthVariance = Random(start: 0f, end: 0f),
                            ColorVariance = Random(start: 0f, end: 0f)
                        }
                    },
                    Trail = new TrailDef
                    {
                        Enable = false,
                        Textures = new[] {
							"",
                        },
                        TextureMode = Normal,
                        DecayTime = 128,
                        Color = Color(red: 0, green: 0, blue: 1, alpha: 1),
                        Back = false,
                        CustomWidth = 0,
                        UseWidthVariance = false,
                        UseColorFade = true,
                    },
                    OffsetEffect = new OffsetEffectDef
                    {
                        MaxOffset = 0,// 0 offset value disables this effect
                        MinLength = 0.2f,
                        MaxLength = 3,
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                TravelSound = "",
                HitSound = "",
                ShieldHitSound = "",
                PlayerHitSound = "",
                VoxelHitSound = "",
                FloatingHitSound = "",
                HitPlayChance = 0.5f,
                HitPlayShield = true,
            }, // Don't edit below this line
            Ejection = new EjectionDef
            {
                Type = Particle, // Particle or Item (Inventory Component)
                Speed = 100f, // Speed inventory is ejected from in dummy direction
                SpawnChance = 0.5f, // chance of triggering effect (0 - 1)
                CompDef = new ComponentDef
                {
                    ItemName = "", //InventoryComponent name
                    ItemLifeTime = 0, // how long item should exist in world
                    Delay = 0, // delay in ticks after shot before ejected
                }
            },
        };
		
		private AmmoDef Flak800 => new AmmoDef
        {
            AmmoMagazine = "FlakBelt",
            AmmoRound = "800m",
            HybridRound = false, //AmmoMagazine based weapon with energy cost
            EnergyCost = 0.00000000001f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
            BaseDamage = 200f,
            Mass = 11f, // in kilograms
            Health = 0, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying.
            BackKickForce = 1000f,
            DecayPerShot = 0f,
            HardPointUsable = true, // set to false if this is a Fragment ammoType and you don't want the turret to be able to select it directly.
            EnergyMagazineSize = 12, //ONLY USE IF "AmmoMagazine=ENERGY". Otherwise specify magazine size in Ammo.sbc file.
            IgnoreWater = false,

            Shape = new ShapeDef //defines the collision shape of projectile, defaults line and visual Line Length if set to 0
            {
                Shape = LineShape, // LineShape or SphereShape. Do not use SphereShape for fast moving projectiles if you care about precision.
                Diameter = 1, // Diameter is minimum length of LineShape or minimum diameter of SphereShape
            },
            ObjectsHit = new ObjectsHitDef
            {
                MaxObjectsHit = 0, // 0 = disabled
                CountBlocks = false, // counts gridBlocks and not just entities hit
            },
            Fragment = new FragmentDef
            {
                AmmoRound = "FlakShrapnel",
                Fragments = 20,
                Degrees = 200,
                Reverse = false,
                RandomizeDir = false, // randomize between forward and backward directions
            },
            Pattern = new PatternDef
            {
                Patterns = new[] {
                    "",
                },
                Enable = false,
                TriggerChance = 1f,
                Random = false,
                RandomMin = 1,
                RandomMax = 1,
                SkipParent = false,
                PatternSteps = 1, // Number of Patterns activated per round, will progress in order and loop.  Ignored if Random = true.
            },
            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0f, // 0 = disabled, 1000 = any blocks with currently integrity above 1000 will be immune to damage.
                DamageVoxels = false, // true = voxels are vulnerable to this weapon
                SelfDamage = false, // true = allow self damage.
                HealthHitModifier = 1.2, // defaults to a value of 1, this setting modifies how much Health is subtracted from a projectile per hit (1 = per hit).
                VoxelHitModifier = 10,
                Characters = 1f,
                // modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01 = 1% damage, 2 = 200% damage.
                FallOff = new FallOffDef
                {
                    Distance = 1000f, // Distance at which max damage begins falling off.
                    MinMultipler = 0f, // value from 0.0f to 1f where 0.1f would be a min damage of 10% of max damage.
                },
                Grids = new GridSizeDef
                    {
                        Large = .5f,
                        Small = 1f,
                    },
                Armor = new ArmorDef
                {
                    Armor = 1f,
                    Light = -1f,
                    Heavy = 1f,
                    NonArmor = -1f,
                },
                Shields = new ShieldDef
                {
                    Modifier = 1f,
                    Type = Default,
                    BypassModifier = -1f,
                },
                // first true/false (ignoreOthers) will cause projectiles to pass through all blocks that do not match the custom subtypeIds.
                Custom = new CustomScalesDef
                {
                    IgnoreAllOthers = false,
                    Types = new[]
                    {
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test1",
                            Modifier = -1f,
                        },
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test2",
                            Modifier = -1f,
                        },
                    },
                },
            },
            AreaEffect = new AreaDamageDef
            {
                AreaEffect = Explosive, // Disabled = do not use area effect at all, Explosive, Radiant, AntiSmart, JumpNullField, JumpNullField, EnergySinkField, AnchorField, EmpField, OffenseField, NavField, DotField.
                Base = new AreaInfluence
                {
                    Radius = 10f, // the sphere of influence of area effects
                    EffectStrength = 0f, // For ewar it applies this amount per pulse/hit, non-ewar applies this as damage per tick per entity in area of influence. For radiant 0 == use spillover from BaseDamage, otherwise use this value.
                },
                Pulse = new PulseDef // interval measured in game ticks (60 == 1 second), pulseChance chance (0 - 100) that an entity in field will be hit
                {
                    Interval = 0,
                    PulseChance = 0,
                    GrowTime = 0,
                    HideModel = false,
                    ShowParticle = false,
                    Particle = new ParticleDef
                    {
                        Name = "", //ShipWelderArc
                        ShrinkByDistance = false,
                        Color = Color(red: 128, green: 0, blue: 0, alpha: 32),
                        Offset = Vector(x: 0, y: -1, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 1,
                            Scale = 1,
                        },
                    },
                },
                Explosions = new ExplosionDef
                {
                    NoVisuals = false,
                    NoSound = false,
                    NoShrapnel = false,
                    NoDeformation = true,
                    Scale = 1,
                    CustomParticle = "",
                    CustomSound = "",
                },
                Detonation = new DetonateDef
                {
                    DetonateOnEnd = true,
                    ArmOnlyOnHit = false,
                    DetonationDamage = 60,
                    DetonationRadius = 1,
                    MinArmingTime = 0, //Min time in ticks before projectile will arm for detonation (will also affect Fragment spawning)
                },
                EwarFields = new EwarFieldsDef
                {
                    Duration = 1,
                    StackDuration = false,
                    Depletable = false,
                    MaxStacks = 0,
                    TriggerRange = 0f,
                    DisableParticleEffect = true,
                    Force = new PushPullDef // AreaEffectDamage is multiplied by target mass.
                    {
                        ForceFrom = ProjectileLastPosition, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
                        ForceTo = HitPosition, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
                        Position = TargetCenterOfMass, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
                    },
                },
            },
            Beams = new BeamDef
            {
                Enable = false,
                VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
                ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
                RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
                OneParticle = false, // Only spawn one particle hit per beam weapon.
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = None,
                TargetLossDegree = 80f,
                TargetLossTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                MaxLifeTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                AccelPerSec = 0f,
                DesiredSpeed = 400, // DO NOT SET HIGHER THAN 4100
                MaxTrajectory = 5000f,
                FieldTime = 0, // 0 is disabled, a value causes the projectile to come to rest, spawn a field and remain for a time (Measured in game ticks, 60 = 1 second)
                GravityMultiplier = 0f, // Gravity multiplier, influences the trajectory of the projectile, value greater than 0 to enable.
                SpeedVariance = Random(start: 0, end: 0), // subtracts value from DesiredSpeed
                RangeVariance = Random(start: 0, end: 0), // subtracts value from MaxTrajectory
                MaxTrajectoryTime = 0, // How long the weapon must fire before it reaches MaxTrajectory.
                Smarts = new SmartsDef
                {
                    Inaccuracy = 0f, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
                    Aggressiveness = 1f, // controls how responsive tracking is.
                    MaxLateralThrust = 0.5, // controls how sharp the trajectile may turn
                    TrackingDelay = 0, // Measured in Shape diameter units traveled.
                    MaxChaseTime = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    OverideTarget = true, // when set to true ammo picks its own target, does not use hardpoint's.
                    MaxTargets = 0, // Number of targets allowed before ending, 0 = unlimited
                    NoTargetExpire = false, // Expire without ever having a target at TargetLossTime
                    Roam = false, // Roam current area after target loss
                    KeepAliveAfterTargetLoss = false, // Whether to stop early death of projectile on target loss
                },
                Mines = new MinesDef
                {
                    DetectRadius = 0,
                    DeCloakRadius = 0,
                    FieldTime = 0,
                    Cloak = false,
                    Persist = false,
                },
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "\\Models\\Akiad\\Small\\FlakRound.mwm",
                VisualProbability = 1f,
                ShieldHitDraw = false,
                Particles = new AmmoParticleDef
                {
                    Ammo = new ParticleDef
                    {
                        Name = "", //ShipWelderArc
                        ShrinkByDistance = false,
                        Color = Color(red: 128, green: 0, blue: 0, alpha: 32),
                        Offset = Vector(x: 0, y: -1, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 1,
                            Scale = 1,
                        },
                    },
                    Hit = new ParticleDef
                    {
                        Name = "",
                        ApplyToShield = true,
                        ShrinkByDistance = false,
                        Color = Color(red: 3, green: 1.9f, blue: 1f, alpha: 1),
                        Offset = Vector(x: 0, y: 0, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 30,
                            Scale = 1,
                            HitPlayChance = 1f,
                        },
                    },
                    Eject = new ParticleDef
                    {
                        Name = "",
                        ApplyToShield = true,
                        ShrinkByDistance = false,
                        Color = Color(red: 3, green: 1.9f, blue: 1f, alpha: 1),
                        Offset = Vector(x: 0, y: 0, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 30,
                            Scale = 1,
                            HitPlayChance = 1f,
                        },
                    },
                },
                Lines = new LineDef
                {
                    ColorVariance = Random(start: 0.75f, end: 2f), // multiply the color by random values within range.
                    WidthVariance = Random(start: 0f, end: 0f), // adds random value to default width (negatives shrinks width)
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = 1f,
                        Width = 0.2f,
                        Color = Color(red: 3, green: 2, blue: 1f, alpha: 1),
                        VisualFadeStart = 0, // Number of ticks the weapon has been firing before projectiles begin to fade their color
                        VisualFadeEnd = 0, // How many ticks after fade began before it will be invisible.
                        Textures = new[] {// WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                            "WeaponLaser",
                        },
                        TextureMode = Normal, // Normal, Cycle, Chaos, Wave
                        Segmentation = new SegmentDef
                        {
                            Enable = false, // If true Tracer TextureMode is ignored
                            Textures = new[] {
								"",
                            },
                            SegmentLength = 0f, // Uses the values below.
                            SegmentGap = 0f, // Uses Tracer textures and values
                            Speed = 1f, // meters per second
                            Color = Color(red: 1, green: 2, blue: 2.5f, alpha: 1),
                            WidthMultiplier = 1f,
                            Reverse = false,
                            UseLineVariance = true,
                            WidthVariance = Random(start: 0f, end: 0f),
                            ColorVariance = Random(start: 0f, end: 0f)
                        }
                    },
                    Trail = new TrailDef
                    {
                        Enable = false,
                        Textures = new[] {
							"",
                        },
                        TextureMode = Normal,
                        DecayTime = 128,
                        Color = Color(red: 0, green: 0, blue: 1, alpha: 1),
                        Back = false,
                        CustomWidth = 0,
                        UseWidthVariance = false,
                        UseColorFade = true,
                    },
                    OffsetEffect = new OffsetEffectDef
                    {
                        MaxOffset = 0,// 0 offset value disables this effect
                        MinLength = 0.2f,
                        MaxLength = 3,
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                TravelSound = "",
                HitSound = "",
                ShieldHitSound = "",
                PlayerHitSound = "",
                VoxelHitSound = "",
                FloatingHitSound = "",
                HitPlayChance = 0.5f,
                HitPlayShield = true,
            }, // Don't edit below this line
            Ejection = new EjectionDef
            {
                Type = Particle, // Particle or Item (Inventory Component)
                Speed = 100f, // Speed inventory is ejected from in dummy direction
                SpawnChance = 0.5f, // chance of triggering effect (0 - 1)
                CompDef = new ComponentDef
                {
                    ItemName = "", //InventoryComponent name
                    ItemLifeTime = 0, // how long item should exist in world
                    Delay = 0, // delay in ticks after shot before ejected
                }
            },
        };
		
		private AmmoDef CenturianShell => new AmmoDef
        {
            AmmoMagazine = "CenturianShell",
            AmmoRound = "CenturianShell",
            HybridRound = false, //AmmoMagazine based weapon with energy cost
            EnergyCost = 0.00000000001f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
            BaseDamage = 2500f,
            Mass = 80f, // in kilograms
            Health = 0, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying.
            BackKickForce = 18000f,
            DecayPerShot = 0f,
            HardPointUsable = true, // set to false if this is a Fragment ammoType and you don't want the turret to be able to select it directly.
            EnergyMagazineSize = 12, //ONLY USE IF "AmmoMagazine=ENERGY". Otherwise specify magazine size in Ammo.sbc file.
            IgnoreWater = false,

            Shape = new ShapeDef //defines the collision shape of projectile, defaults line and visual Line Length if set to 0
            {
                Shape = LineShape, // LineShape or SphereShape. Do not use SphereShape for fast moving projectiles if you care about precision.
                Diameter = 1, // Diameter is minimum length of LineShape or minimum diameter of SphereShape
            },
            ObjectsHit = new ObjectsHitDef
            {
                MaxObjectsHit = 0, // 0 = disabled
                CountBlocks = false, // counts gridBlocks and not just entities hit
            },
            Fragment = new FragmentDef
            {
                AmmoRound = "HEShrapnel",
                Fragments = 20,
                Degrees = 120,
                Reverse = false,
                RandomizeDir = false, // randomize between forward and backward directions
            },
            Pattern = new PatternDef
            {
                Patterns = new[] {
                    "",
                },
                Enable = false,
                TriggerChance = 1f,
                Random = false,
                RandomMin = 1,
                RandomMax = 1,
                SkipParent = false,
                PatternSteps = 1, // Number of Patterns activated per round, will progress in order and loop.  Ignored if Random = true.
            },
            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0f, // 0 = disabled, 1000 = any blocks with currently integrity above 1000 will be immune to damage.
                DamageVoxels = false, // true = voxels are vulnerable to this weapon
                SelfDamage = false, // true = allow self damage.
                HealthHitModifier = 40, // defaults to a value of 1, this setting modifies how much Health is subtracted from a projectile per hit (1 = per hit).
                VoxelHitModifier = 10,
                Characters = 1f,
                // modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01 = 1% damage, 2 = 200% damage.
                FallOff = new FallOffDef
                {
                    Distance = 8000f, // Distance at which max damage begins falling off.
                    MinMultipler = 0f, // value from 0.0f to 1f where 0.1f would be a min damage of 10% of max damage.
                },
                Grids = new GridSizeDef
                    {
                        Large = 1f,
                        Small = 1f,
                    },
                Armor = new ArmorDef
                {
                    Armor = 1f,
                    Light = 1f,
                    Heavy = 1f,
                    NonArmor = 1f,
                },
                Shields = new ShieldDef
                {
                    Modifier = 1f,
                    Type = Default,
                    BypassModifier = -1f,
                },
                // first true/false (ignoreOthers) will cause projectiles to pass through all blocks that do not match the custom subtypeIds.
                Custom = new CustomScalesDef
                {
                    IgnoreAllOthers = false,
                    Types = new[]
                    {
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test1",
                            Modifier = -1f,
                        },
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test2",
                            Modifier = -1f,
                        },
                    },
                },
            },
            AreaEffect = new AreaDamageDef
                {
                    AreaEffect = Explosive, // Disabled = do not use area effect at all, Explosive, Radiant, AntiSmart, JumpNullField, JumpNullField, EnergySinkField, AnchorField, EmpField, OffenseField, NavField, DotField.
                    AreaEffectDamage = 0f, // 0 = DO NOT USE FOR EXPLOSIVES, USE DETONATION SETTINGS INSTEAD
                    AreaEffectRadius = 0f, // DO NOT USE FOR EXPLOSIVES, USE DETONATION SETTINGS INSTEAD
                    Pulse = new PulseDef // interval measured in game ticks (60 == 1 second), pulseChance chance (0 - 100) that an entity in field will be hit
                    {
                        Interval = 30,
                        PulseChance = 0,
                    },
                    Explosions = new ExplosionDef
                    {
                        NoVisuals = false, //Set to false to use vanilla explosion particles, unless specified a custom particle
                        NoSound = false, ////Set to false to use vanilla explosion noises, unless specified a custom sound
                        Scale = 2f, //Scale of the particles.
                        CustomParticle = "",
                        CustomSound = "",
                    },
                    Detonation = new DetonateDef //USE THESE MODIFIERS FOR EXPLOSIONS
                    {
                        DetonateOnEnd = true,
                        ArmOnlyOnHit = false,
                        DetonationDamage = 8000, //1 Steelplate =100HP, 1 large light armor cube=2500HP. Has a natural falloff based on radius.
                        DetonationRadius = 10, //Measured in meters, how large the explosion should be.
                    },
                    EwarFields = new EwarFieldsDef
                    {
                        Duration = 0,
                        StackDuration = true,
                        Depletable = false,
                        MaxStacks = 10,
                        TriggerRange = 5f,
                    },
                },
                Beams = new BeamDef
                {
                    Enable = false,
                    VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
                    ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
                    RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
                    OneParticle = false, // Only spawn one particle hit per beam weapon.
                },
            Trajectory = new TrajectoryDef
            {
                Guidance = None,
                TargetLossDegree = 80f,
                TargetLossTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                MaxLifeTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                AccelPerSec = 0f,
                DesiredSpeed = 800, // DO NOT SET HIGHER THAN 4100
                MaxTrajectory = 5200f,
                FieldTime = 0, // 0 is disabled, a value causes the projectile to come to rest, spawn a field and remain for a time (Measured in game ticks, 60 = 1 second)
                GravityMultiplier = 1f, // Gravity multiplier, influences the trajectory of the projectile, value greater than 0 to enable.
                SpeedVariance = Random(start: 0, end: 0), // subtracts value from DesiredSpeed
                RangeVariance = Random(start: 0, end: 0), // subtracts value from MaxTrajectory
                MaxTrajectoryTime = 0, // How long the weapon must fire before it reaches MaxTrajectory.
                Smarts = new SmartsDef
                {
                    Inaccuracy = 0f, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
                    Aggressiveness = 1f, // controls how responsive tracking is.
                    MaxLateralThrust = 0.5, // controls how sharp the trajectile may turn
                    TrackingDelay = 0, // Measured in Shape diameter units traveled.
                    MaxChaseTime = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    OverideTarget = true, // when set to true ammo picks its own target, does not use hardpoint's.
                    MaxTargets = 0, // Number of targets allowed before ending, 0 = unlimited
                    NoTargetExpire = false, // Expire without ever having a target at TargetLossTime
                    Roam = false, // Roam current area after target loss
                    KeepAliveAfterTargetLoss = false, // Whether to stop early death of projectile on target loss
                },
                Mines = new MinesDef
                {
                    DetectRadius = 0,
                    DeCloakRadius = 0,
                    FieldTime = 0,
                    Cloak = false,
                    Persist = false,
                },
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "\\Models\\Akiad\\Small\\CenturianShell.mwm",
                VisualProbability = 1f,
                ShieldHitDraw = false,
                Particles = new AmmoParticleDef
                {
                    Ammo = new ParticleDef
                    {
                        Name = "", //ShipWelderArc
                        ShrinkByDistance = false,
                        Color = Color(red: 128, green: 0, blue: 0, alpha: 32),
                        Offset = Vector(x: 0, y: -1, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 1,
                            Scale = 1,
                        },
                    },
                    Hit = new ParticleDef
                    {
                        Name = "",
                        ApplyToShield = true,
                        ShrinkByDistance = false,
                        Color = Color(red: 3, green: 1.9f, blue: 1f, alpha: 1),
                        Offset = Vector(x: 0, y: 0, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 30,
                            Scale = 1,
                            HitPlayChance = 1f,
                        },
                    },
                    Eject = new ParticleDef
                    {
                        Name = "Smoke_Collector",
                        ApplyToShield = true,
                        ShrinkByDistance = false,
                        Color = Color(red: 3, green: 1.9f, blue: 1f, alpha: 1),
                        Offset = Vector(x: 0, y: 0, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 30,
                            Scale = 1,
                            HitPlayChance = 1f,
                        },
                    },
                },
                Lines = new LineDef
                {
                    ColorVariance = Random(start: 0.75f, end: 2f), // multiply the color by random values within range.
                    WidthVariance = Random(start: 0f, end: 0f), // adds random value to default width (negatives shrinks width)
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = .0001f,
                        Width = 0.0001f,
                        Color = Color(red: 3, green: 2, blue: 1f, alpha: 1),
                        VisualFadeStart = 0, // Number of ticks the weapon has been firing before projectiles begin to fade their color
                        VisualFadeEnd = 0, // How many ticks after fade began before it will be invisible.
                        Textures = new[] {// WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                            "WeaponLaser",
                        },
                        TextureMode = Normal, // Normal, Cycle, Chaos, Wave
                        Segmentation = new SegmentDef
                        {
                            Enable = false, // If true Tracer TextureMode is ignored
                            Textures = new[] {
								"",
                            },
                            SegmentLength = 0f, // Uses the values below.
                            SegmentGap = 0f, // Uses Tracer textures and values
                            Speed = 1f, // meters per second
                            Color = Color(red: 1, green: 2, blue: 2.5f, alpha: 1),
                            WidthMultiplier = 1f,
                            Reverse = false,
                            UseLineVariance = true,
                            WidthVariance = Random(start: 0f, end: 0f),
                            ColorVariance = Random(start: 0f, end: 0f)
                        }
                    },
                    Trail = new TrailDef
                    {
                        Enable = false,
                        Textures = new[] {
							"",
                        },
                        TextureMode = Normal,
                        DecayTime = 128,
                        Color = Color(red: 0, green: 0, blue: 1, alpha: 1),
                        Back = false,
                        CustomWidth = 0,
                        UseWidthVariance = false,
                        UseColorFade = true,
                    },
                    OffsetEffect = new OffsetEffectDef
                    {
                        MaxOffset = 0,// 0 offset value disables this effect
                        MinLength = 0.2f,
                        MaxLength = 3,
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                TravelSound = "",
                HitSound = "",
                ShieldHitSound = "",
                PlayerHitSound = "",
                VoxelHitSound = "",
                FloatingHitSound = "",
                HitPlayChance = 0.5f,
                HitPlayShield = true,
            }, // Don't edit below this line
            Ejection = new EjectionDef
            {
                Type = Item, // Particle or Item (Inventory Component)
                Speed = 10f, // Speed inventory is ejected from in dummy direction
                SpawnChance = 1f, // chance of triggering effect (0 - 1)
                CompDef = new ComponentDef
                {
                    ItemName = "CenturianCasing", //InventoryComponent name
                    ItemLifeTime = 300, // how long item should exist in world
                    Delay = 10, // delay in ticks after shot before ejected
                }
            },
        };
		private AmmoDef CerberusStack => new AmmoDef
        {
            AmmoMagazine = "CerberusStack",
            AmmoRound = "CenturianShell",
            HybridRound = false, //AmmoMagazine based weapon with energy cost
            EnergyCost = 0.00000000001f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
            BaseDamage = 2500f,
            Mass = 80f, // in kilograms
            Health = 0, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying.
            BackKickForce = 10000f,
            DecayPerShot = 0f,
            HardPointUsable = true, // set to false if this is a Fragment ammoType and you don't want the turret to be able to select it directly.
            EnergyMagazineSize = 12, //ONLY USE IF "AmmoMagazine=ENERGY". Otherwise specify magazine size in Ammo.sbc file.
            IgnoreWater = false,

            Shape = new ShapeDef //defines the collision shape of projectile, defaults line and visual Line Length if set to 0
            {
                Shape = LineShape, // LineShape or SphereShape. Do not use SphereShape for fast moving projectiles if you care about precision.
                Diameter = 1, // Diameter is minimum length of LineShape or minimum diameter of SphereShape
            },
            ObjectsHit = new ObjectsHitDef
            {
                MaxObjectsHit = 0, // 0 = disabled
                CountBlocks = false, // counts gridBlocks and not just entities hit
            },
            Fragment = new FragmentDef
            {
                AmmoRound = "HEShrapnel",
                Fragments = 20,
                Degrees = 120,
                Reverse = false,
                RandomizeDir = false, // randomize between forward and backward directions
            },
            Pattern = new PatternDef
            {
                Patterns = new[] {
                    "",
                },
                Enable = false,
                TriggerChance = 1f,
                Random = false,
                RandomMin = 1,
                RandomMax = 1,
                SkipParent = false,
                PatternSteps = 1, // Number of Patterns activated per round, will progress in order and loop.  Ignored if Random = true.
            },
            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0f, // 0 = disabled, 1000 = any blocks with currently integrity above 1000 will be immune to damage.
                DamageVoxels = false, // true = voxels are vulnerable to this weapon
                SelfDamage = false, // true = allow self damage.
                HealthHitModifier = 2, // defaults to a value of 1, this setting modifies how much Health is subtracted from a projectile per hit (1 = per hit).
                VoxelHitModifier = 10,
                Characters = 1f,
                // modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01 = 1% damage, 2 = 200% damage.
                FallOff = new FallOffDef
                {
                    Distance = 8000f, // Distance at which max damage begins falling off.
                    MinMultipler = 0f, // value from 0.0f to 1f where 0.1f would be a min damage of 10% of max damage.
                },
                Grids = new GridSizeDef
                    {
                        Large = 1f,
                        Small = -1f,
                    },
                Armor = new ArmorDef
                {
                    Armor = 1f,
                    Light = 1f,
                    Heavy = -1f,
                    NonArmor = 1f,
                },
                Shields = new ShieldDef
                {
                    Modifier = 1f,
                    Type = Default,
                    BypassModifier = -1f,
                },
                // first true/false (ignoreOthers) will cause projectiles to pass through all blocks that do not match the custom subtypeIds.
                Custom = new CustomScalesDef
                {
                    IgnoreAllOthers = false,
                    Types = new[]
                    {
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test1",
                            Modifier = -1f,
                        },
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test2",
                            Modifier = -1f,
                        },
                    },
                },
            },
            AreaEffect = new AreaDamageDef
                {
                    AreaEffect = Explosive, // Disabled = do not use area effect at all, Explosive, Radiant, AntiSmart, JumpNullField, JumpNullField, EnergySinkField, AnchorField, EmpField, OffenseField, NavField, DotField.
                    AreaEffectDamage = 0f, // 0 = DO NOT USE FOR EXPLOSIVES, USE DETONATION SETTINGS INSTEAD
                    AreaEffectRadius = 0f, // DO NOT USE FOR EXPLOSIVES, USE DETONATION SETTINGS INSTEAD
                    Pulse = new PulseDef // interval measured in game ticks (60 == 1 second), pulseChance chance (0 - 100) that an entity in field will be hit
                    {
                        Interval = 30,
                        PulseChance = 0,
                    },
                    Explosions = new ExplosionDef
                    {
                        NoVisuals = false, //Set to false to use vanilla explosion particles, unless specified a custom particle
                        NoSound = false, ////Set to false to use vanilla explosion noises, unless specified a custom sound
                        Scale = 2f, //Scale of the particles.
                        CustomParticle = "",
                        CustomSound = "",
                    },
                    Detonation = new DetonateDef //USE THESE MODIFIERS FOR EXPLOSIONS
                    {
                        DetonateOnEnd = true,
                        ArmOnlyOnHit = false,
                        DetonationDamage = 500, //1 Steelplate =100HP, 1 large light armor cube=2500HP. Has a natural falloff based on radius.
                        DetonationRadius = 10, //Measured in meters, how large the explosion should be.
                    },
                    EwarFields = new EwarFieldsDef
                    {
                        Duration = 0,
                        StackDuration = true,
                        Depletable = false,
                        MaxStacks = 10,
                        TriggerRange = 5f,
                    },
                },
                Beams = new BeamDef
                {
                    Enable = false,
                    VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
                    ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
                    RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
                    OneParticle = false, // Only spawn one particle hit per beam weapon.
                },
            Trajectory = new TrajectoryDef
            {
                Guidance = None,
                TargetLossDegree = 80f,
                TargetLossTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                MaxLifeTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                AccelPerSec = 0f,
                DesiredSpeed = 800, // DO NOT SET HIGHER THAN 4100
                MaxTrajectory = 5200f,
                FieldTime = 0, // 0 is disabled, a value causes the projectile to come to rest, spawn a field and remain for a time (Measured in game ticks, 60 = 1 second)
                GravityMultiplier = 1f, // Gravity multiplier, influences the trajectory of the projectile, value greater than 0 to enable.
                SpeedVariance = Random(start: 0, end: 0), // subtracts value from DesiredSpeed
                RangeVariance = Random(start: 0, end: 0), // subtracts value from MaxTrajectory
                MaxTrajectoryTime = 0, // How long the weapon must fire before it reaches MaxTrajectory.
                Smarts = new SmartsDef
                {
                    Inaccuracy = 0f, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
                    Aggressiveness = 1f, // controls how responsive tracking is.
                    MaxLateralThrust = 0.5, // controls how sharp the trajectile may turn
                    TrackingDelay = 0, // Measured in Shape diameter units traveled.
                    MaxChaseTime = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    OverideTarget = true, // when set to true ammo picks its own target, does not use hardpoint's.
                    MaxTargets = 0, // Number of targets allowed before ending, 0 = unlimited
                    NoTargetExpire = false, // Expire without ever having a target at TargetLossTime
                    Roam = false, // Roam current area after target loss
                    KeepAliveAfterTargetLoss = false, // Whether to stop early death of projectile on target loss
                },
                Mines = new MinesDef
                {
                    DetectRadius = 0,
                    DeCloakRadius = 0,
                    FieldTime = 0,
                    Cloak = false,
                    Persist = false,
                },
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "\\Models\\Akiad\\Small\\CenturianShell.mwm",
                VisualProbability = 1f,
                ShieldHitDraw = false,
                Particles = new AmmoParticleDef
                {
                    Ammo = new ParticleDef
                    {
                        Name = "", //ShipWelderArc
                        ShrinkByDistance = false,
                        Color = Color(red: 128, green: 0, blue: 0, alpha: 32),
                        Offset = Vector(x: 0, y: -1, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 1,
                            Scale = 1,
                        },
                    },
                    Hit = new ParticleDef
                    {
                        Name = "",
                        ApplyToShield = true,
                        ShrinkByDistance = false,
                        Color = Color(red: 3, green: 1.9f, blue: 1f, alpha: 1),
                        Offset = Vector(x: 0, y: 0, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 30,
                            Scale = 1,
                            HitPlayChance = 1f,
                        },
                    },
                    Eject = new ParticleDef
                    {
                        Name = "Smoke_Collector",
                        ApplyToShield = true,
                        ShrinkByDistance = false,
                        Color = Color(red: 3, green: 1.9f, blue: 1f, alpha: 1),
                        Offset = Vector(x: 0, y: 0, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 30,
                            Scale = 1,
                            HitPlayChance = 1f,
                        },
                    },
                },
                Lines = new LineDef
                {
                    ColorVariance = Random(start: 0.75f, end: 2f), // multiply the color by random values within range.
                    WidthVariance = Random(start: 0f, end: 0f), // adds random value to default width (negatives shrinks width)
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = .0001f,
                        Width = 0.0001f,
                        Color = Color(red: 3, green: 2, blue: 1f, alpha: 1),
                        VisualFadeStart = 0, // Number of ticks the weapon has been firing before projectiles begin to fade their color
                        VisualFadeEnd = 0, // How many ticks after fade began before it will be invisible.
                        Textures = new[] {// WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                            "WeaponLaser",
                        },
                        TextureMode = Normal, // Normal, Cycle, Chaos, Wave
                        Segmentation = new SegmentDef
                        {
                            Enable = false, // If true Tracer TextureMode is ignored
                            Textures = new[] {
								"",
                            },
                            SegmentLength = 0f, // Uses the values below.
                            SegmentGap = 0f, // Uses Tracer textures and values
                            Speed = 1f, // meters per second
                            Color = Color(red: 1, green: 2, blue: 2.5f, alpha: 1),
                            WidthMultiplier = 1f,
                            Reverse = false,
                            UseLineVariance = true,
                            WidthVariance = Random(start: 0f, end: 0f),
                            ColorVariance = Random(start: 0f, end: 0f)
                        }
                    },
                    Trail = new TrailDef
                    {
                        Enable = false,
                        Textures = new[] {
							"",
                        },
                        TextureMode = Normal,
                        DecayTime = 128,
                        Color = Color(red: 0, green: 0, blue: 1, alpha: 1),
                        Back = false,
                        CustomWidth = 0,
                        UseWidthVariance = false,
                        UseColorFade = true,
                    },
                    OffsetEffect = new OffsetEffectDef
                    {
                        MaxOffset = 0,// 0 offset value disables this effect
                        MinLength = 0.2f,
                        MaxLength = 3,
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                TravelSound = "",
                HitSound = "",
                ShieldHitSound = "",
                PlayerHitSound = "",
                VoxelHitSound = "",
                FloatingHitSound = "",
                HitPlayChance = 0.5f,
                HitPlayShield = true,
            }, // Don't edit below this line
            Ejection = new EjectionDef
            {
                Type = Item, // Particle or Item (Inventory Component)
                Speed = 10f, // Speed inventory is ejected from in dummy direction
                SpawnChance = 1f, // chance of triggering effect (0 - 1)
                CompDef = new ComponentDef
                {
                    ItemName = "CenturianCasing", //InventoryComponent name
                    ItemLifeTime = 300, // how long item should exist in world
                    Delay = 10, // delay in ticks after shot before ejected
                }
            },
        };
		
		private AmmoDef Raptor200x30 => new AmmoDef
        {
            AmmoMagazine = "Raptor200x30",
            AmmoRound = "30mm HEI",
            HybridRound = false, //AmmoMagazine based weapon with energy cost
            EnergyCost = 0.00000000001f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
            BaseDamage = 2000f,
            Mass = 0.271f, // in kilograms
            Health = 0, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying.
            BackKickForce = 1900f,
            DecayPerShot = 0f,
            HardPointUsable = true, // set to false if this is a Fragment ammoType and you don't want the turret to be able to select it directly.
            EnergyMagazineSize = 12, //ONLY USE IF "AmmoMagazine=ENERGY". Otherwise specify magazine size in Ammo.sbc file.
            IgnoreWater = false,

            Shape = new ShapeDef //defines the collision shape of projectile, defaults line and visual Line Length if set to 0
            {
                Shape = LineShape, // LineShape or SphereShape. Do not use SphereShape for fast moving projectiles if you care about precision.
                Diameter = 1, // Diameter is minimum length of LineShape or minimum diameter of SphereShape
            },
            ObjectsHit = new ObjectsHitDef
            {
                MaxObjectsHit = 0, // 0 = disabled
                CountBlocks = false, // counts gridBlocks and not just entities hit
            },
            Fragment = new FragmentDef
            {
                AmmoRound = "",
                Fragments = 0,
                Degrees = 15,
                Reverse = false,
                RandomizeDir = false, // randomize between forward and backward directions
            },
            Pattern = new PatternDef
            {
                Patterns = new[] {
                    "",
                },
                Enable = false,
                TriggerChance = 1f,
                Random = false,
                RandomMin = 1,
                RandomMax = 1,
                SkipParent = false,
                PatternSteps = 1, // Number of Patterns activated per round, will progress in order and loop.  Ignored if Random = true.
            },
            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0f, // 0 = disabled, 1000 = any blocks with currently integrity above 1000 will be immune to damage.
                DamageVoxels = false, // true = voxels are vulnerable to this weapon
                SelfDamage = false, // true = allow self damage.
                HealthHitModifier = 3, // defaults to a value of 1, this setting modifies how much Health is subtracted from a projectile per hit (1 = per hit).
                VoxelHitModifier = 10,
                Characters = 1f,
                // modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01 = 1% damage, 2 = 200% damage.
                FallOff = new FallOffDef
                {
                    Distance = 1200f, // Distance at which max damage begins falling off.
                    MinMultipler = 0f, // value from 0.0f to 1f where 0.1f would be a min damage of 10% of max damage.
                },
                Grids = new GridSizeDef
                    {
                        Large = 1f,
                        Small = 1f,
                    },
                Armor = new ArmorDef
                {
                    Armor = 1f,
                    Light = -1f,
                    Heavy = 1f,
                    NonArmor = -1f,
                },
                Shields = new ShieldDef
                {
                    Modifier = 1f,
                    Type = Default,
                    BypassModifier = -1f,
                },
                // first true/false (ignoreOthers) will cause projectiles to pass through all blocks that do not match the custom subtypeIds.
                Custom = new CustomScalesDef
                {
                    IgnoreAllOthers = false,
                    Types = new[]
                    {
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test1",
                            Modifier = -1f,
                        },
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test2",
                            Modifier = -1f,
                        },
                    },
                },
            },
            AreaEffect = new AreaDamageDef
            {
                AreaEffect = Explosive, // Disabled = do not use area effect at all, Explosive, Radiant, AntiSmart, JumpNullField, JumpNullField, EnergySinkField, AnchorField, EmpField, OffenseField, NavField, DotField.
                Base = new AreaInfluence
                {
                    Radius = .05f, // the sphere of influence of area effects
                    EffectStrength = 0f, // For ewar it applies this amount per pulse/hit, non-ewar applies this as damage per tick per entity in area of influence. For radiant 0 == use spillover from BaseDamage, otherwise use this value.
                },
                Pulse = new PulseDef // interval measured in game ticks (60 == 1 second), pulseChance chance (0 - 100) that an entity in field will be hit
                {
                    Interval = 0,
                    PulseChance = 0,
                    GrowTime = 0,
                    HideModel = false,
                    ShowParticle = false,
                    Particle = new ParticleDef
                    {
                        Name = "", //ShipWelderArc
                        ShrinkByDistance = false,
                        Color = Color(red: 128, green: 0, blue: 0, alpha: 32),
                        Offset = Vector(x: 0, y: -1, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 1,
                            Scale = 1,
                        },
                    },
                },
                Explosions = new ExplosionDef
                {
                    NoVisuals = false,
                    NoSound = false,
                    NoShrapnel = false,
                    NoDeformation = true,
                    Scale = .05f,
                    CustomParticle = "",
                    CustomSound = "",
                },
                Detonation = new DetonateDef
                {
                    DetonateOnEnd = true,
                    ArmOnlyOnHit = false,
                    DetonationDamage = 1,
                    DetonationRadius = 0,
                    MinArmingTime = 0, //Min time in ticks before projectile will arm for detonation (will also affect Fragment spawning)
                },
                EwarFields = new EwarFieldsDef
                {
                    Duration = 1,
                    StackDuration = false,
                    Depletable = false,
                    MaxStacks = 0,
                    TriggerRange = 0f,
                    DisableParticleEffect = true,
                    Force = new PushPullDef // AreaEffectDamage is multiplied by target mass.
                    {
                        ForceFrom = ProjectileLastPosition, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
                        ForceTo = HitPosition, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
                        Position = TargetCenterOfMass, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
                    },
                },
            },
            Beams = new BeamDef
            {
                Enable = false,
                VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
                ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
                RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
                OneParticle = false, // Only spawn one particle hit per beam weapon.
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = None,
                TargetLossDegree = 80f,
                TargetLossTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                MaxLifeTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                AccelPerSec = 0f,
                DesiredSpeed = 700, // DO NOT SET HIGHER THAN 4100
                MaxTrajectory = 5200f,
                FieldTime = 0, // 0 is disabled, a value causes the projectile to come to rest, spawn a field and remain for a time (Measured in game ticks, 60 = 1 second)
                GravityMultiplier = 0f, // Gravity multiplier, influences the trajectory of the projectile, value greater than 0 to enable.
                SpeedVariance = Random(start: 0, end: 0), // subtracts value from DesiredSpeed
                RangeVariance = Random(start: 0, end: 0), // subtracts value from MaxTrajectory
                MaxTrajectoryTime = 0, // How long the weapon must fire before it reaches MaxTrajectory.
                Smarts = new SmartsDef
                {
                    Inaccuracy = 0f, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
                    Aggressiveness = 1f, // controls how responsive tracking is.
                    MaxLateralThrust = 0.5, // controls how sharp the trajectile may turn
                    TrackingDelay = 0, // Measured in Shape diameter units traveled.
                    MaxChaseTime = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    OverideTarget = true, // when set to true ammo picks its own target, does not use hardpoint's.
                    MaxTargets = 0, // Number of targets allowed before ending, 0 = unlimited
                    NoTargetExpire = false, // Expire without ever having a target at TargetLossTime
                    Roam = false, // Roam current area after target loss
                    KeepAliveAfterTargetLoss = false, // Whether to stop early death of projectile on target loss
                },
                Mines = new MinesDef
                {
                    DetectRadius = 0,
                    DeCloakRadius = 0,
                    FieldTime = 0,
                    Cloak = false,
                    Persist = false,
                },
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "",
                VisualProbability = 1f,
                ShieldHitDraw = false,
                Particles = new AmmoParticleDef
                {
                    Ammo = new ParticleDef
                    {
                        Name = "", //ShipWelderArc
                        ShrinkByDistance = false,
                        Color = Color(red: 208, green: 0, blue: 0, alpha: 32),
                        Offset = Vector(x: 0, y: -1, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 1,
                            Scale = 1,
                        },
                    },
                    Hit = new ParticleDef
                    {
                        Name = "",
                        ApplyToShield = true,
                        ShrinkByDistance = false,
                        Color = Color(red: 3, green: 1.9f, blue: 1f, alpha: 1),
                        Offset = Vector(x: 0, y: 0, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 30,
                            Scale = 1,
                            HitPlayChance = 1f,
                        },
                    },
                    Eject = new ParticleDef
                    {
                        Name = "",
                        ApplyToShield = true,
                        ShrinkByDistance = false,
                        Color = Color(red: 3, green: 1.9f, blue: 1f, alpha: 1),
                        Offset = Vector(x: 0, y: 0, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 30,
                            Scale = 1,
                            HitPlayChance = 1f,
                        },
                    },
                },
                Lines = new LineDef
                {
                    ColorVariance = Random(start: 0.75f, end: 2f), // multiply the color by random values within range.
                    WidthVariance = Random(start: 0f, end: 0f), // adds random value to default width (negatives shrinks width)
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = 1f,
                        Width = 0.2f,
                        Color = Color(red: 30, green: 10, blue: 1f, alpha: 1),
                        VisualFadeStart = 0, // Number of ticks the weapon has been firing before projectiles begin to fade their color
                        VisualFadeEnd = 0, // How many ticks after fade began before it will be invisible.
                        Textures = new[] {// WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                            "WeaponLaser",
                        },
                        TextureMode = Normal, // Normal, Cycle, Chaos, Wave
                        Segmentation = new SegmentDef
                        {
                            Enable = false, // If true Tracer TextureMode is ignored
                            Textures = new[] {
								"",
                            },
                            SegmentLength = 0f, // Uses the values below.
                            SegmentGap = 0f, // Uses Tracer textures and values
                            Speed = 1f, // meters per second
                            Color = Color(red: 10.5f, green: 2, blue: 2.5f, alpha: 1),
                            WidthMultiplier = 1f,
                            Reverse = false,
                            UseLineVariance = true,
                            WidthVariance = Random(start: 0f, end: 0f),
                            ColorVariance = Random(start: 0f, end: 0f)
                        }
                    },
                    Trail = new TrailDef
                    {
                        Enable = false,
                        Textures = new[] {
							"",
                        },
                        TextureMode = Normal,
                        DecayTime = 128,
                        Color = Color(red: 0, green: 0, blue: 1, alpha: 1),
                        Back = false,
                        CustomWidth = 0,
                        UseWidthVariance = false,
                        UseColorFade = true,
                    },
                    OffsetEffect = new OffsetEffectDef
                    {
                        MaxOffset = 0,// 0 offset value disables this effect
                        MinLength = 0.2f,
                        MaxLength = 3,
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                TravelSound = "",
                HitSound = "",
                ShieldHitSound = "",
                PlayerHitSound = "",
                VoxelHitSound = "",
                FloatingHitSound = "",
                HitPlayChance = 0.5f,
                HitPlayShield = true,
            }, // Don't edit below this line
            Ejection = new EjectionDef
            {
                Type = Particle, // Particle or Item (Inventory Component)
                Speed = 100f, // Speed inventory is ejected from in dummy direction
                SpawnChance = 0.5f, // chance of triggering effect (0 - 1)
                CompDef = new ComponentDef
                {
                    ItemName = "", //InventoryComponent name
                    ItemLifeTime = 0, // how long item should exist in world
                    Delay = 0, // delay in ticks after shot before ejected
                }
            },
        };
		
		private AmmoDef CoilGunBurst => new AmmoDef
        {
            AmmoMagazine = "ENERGY",
            AmmoRound = "CoilGunBurst",
            HybridRound = false, //AmmoMagazine based weapon with energy cost
            EnergyCost = 0.0000001f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
            BaseDamage = 2000f,
            Mass = 5f, // in kilograms
            Health = 0, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying.
            BackKickForce = 1000f,
            DecayPerShot = 0f,
            HardPointUsable = false, // set to false if this is a Fragment ammoType and you don't want the turret to be able to select it directly.
            EnergyMagazineSize = 12, //ONLY USE IF "AmmoMagazine=ENERGY". Otherwise specify magazine size in Ammo.sbc file.
            IgnoreWater = false,

            Shape = new ShapeDef //defines the collision shape of projectile, defaults line and visual Line Length if set to 0
            {
                Shape = LineShape, // LineShape or SphereShape. Do not use SphereShape for fast moving projectiles if you care about precision.
                Diameter = 1, // Diameter is minimum length of LineShape or minimum diameter of SphereShape
            },
            ObjectsHit = new ObjectsHitDef
            {
                MaxObjectsHit = 0, // 0 = disabled
                CountBlocks = false, // counts gridBlocks and not just entities hit
            },
            Fragment = new FragmentDef
            {
                AmmoRound = "",
                Fragments = 0,
                Degrees = 15,
                Reverse = false,
                RandomizeDir = false, // randomize between forward and backward directions
            },
            Pattern = new PatternDef
            {
                Patterns = new[] {
                    "",
                },
                Enable = false,
                TriggerChance = 1f,
                Random = false,
                RandomMin = 1,
                RandomMax = 1,
                SkipParent = false,
                PatternSteps = 1, // Number of Patterns activated per round, will progress in order and loop.  Ignored if Random = true.
            },
            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0f, // 0 = disabled, 1000 = any blocks with currently integrity above 1000 will be immune to damage.
                DamageVoxels = false, // true = voxels are vulnerable to this weapon
                SelfDamage = false, // true = allow self damage.
                HealthHitModifier = 1.5, // defaults to a value of 1, this setting modifies how much Health is subtracted from a projectile per hit (1 = per hit).
                VoxelHitModifier = 10,
                Characters = 1f,
                // modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01 = 1% damage, 2 = 200% damage.
                FallOff = new FallOffDef
                {
                    Distance = 0f, // Distance at which max damage begins falling off.
                    MinMultipler = 0f, // value from 0.0f to 1f where 0.1f would be a min damage of 10% of max damage.
                },
                Grids = new GridSizeDef
                {
                    Large = 1f,
                    Small = 1f,
                },
                Armor = new ArmorDef
                {
                    Armor = 1f,
                    Light = 1f,
                    Heavy = 1f,
                    NonArmor = 1f,
                },
                Shields = new ShieldDef
                {
                    Modifier = 3f,
                    Type = Default,
                    BypassModifier = 0.0000001f,
                },
                // first true/false (ignoreOthers) will cause projectiles to pass through all blocks that do not match the custom subtypeIds.
                Custom = new CustomScalesDef
                {
                    IgnoreAllOthers = false,
                    Types = new[]
                    {
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test1",
                            Modifier = -1f,
                        },
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test2",
                            Modifier = -1f,
                        },
                    },
                },
            },
            AreaEffect = new AreaDamageDef
            {
                AreaEffect = Radiant, // Disabled = do not use area effect at all, Explosive, Radiant, AntiSmart, JumpNullField, JumpNullField, EnergySinkField, AnchorField, EmpField, OffenseField, NavField, DotField.
                Base = new AreaInfluence
                {
                    Radius = 10f, // the sphere of influence of area effects
                    EffectStrength = 0f, // For ewar it applies this amount per pulse/hit, non-ewar applies this as damage per tick per entity in area of influence. For radiant 0 == use spillover from BaseDamage, otherwise use this value.
                },
                Pulse = new PulseDef // interval measured in game ticks (60 == 1 second), pulseChance chance (0 - 100) that an entity in field will be hit
                {
                    Interval = 0,
                    PulseChance = 0,
                    GrowTime = 0,
                    HideModel = false,
                    ShowParticle = false,
                    Particle = new ParticleDef
                    {
                        Name = "", //ShipWelderArc
                        ShrinkByDistance = false,
                        Color = Color(red: 128, green: 0, blue: 0, alpha: 32),
                        Offset = Vector(x: 0, y: -1, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 1,
                            Scale = 1,
                        },
                    },
                },
                Explosions = new ExplosionDef
                {
                    NoVisuals = false,
                    NoSound = false,
                    NoShrapnel = false,
                    NoDeformation = true,
                    Scale = 1,
                    CustomParticle = "",
                    CustomSound = "",
                },
                Detonation = new DetonateDef
                {
                    DetonateOnEnd = false,
                    ArmOnlyOnHit = false,
                    DetonationDamage = 0,
                    DetonationRadius = 0,
                    MinArmingTime = 0, //Min time in ticks before projectile will arm for detonation (will also affect Fragment spawning)
                },
                EwarFields = new EwarFieldsDef
                {
                    Duration = 1,
                    StackDuration = false,
                    Depletable = false,
                    MaxStacks = 0,
                    TriggerRange = 0f,
                    DisableParticleEffect = true,
                    Force = new PushPullDef // AreaEffectDamage is multiplied by target mass.
                    {
                        ForceFrom = ProjectileLastPosition, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
                        ForceTo = HitPosition, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
                        Position = TargetCenterOfMass, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
                    },
                },
            },
            Beams = new BeamDef
            {
                Enable = true,
                VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
                ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
                RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
                OneParticle = false, // Only spawn one particle hit per beam weapon.
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = None,
                TargetLossDegree = 80f,
                TargetLossTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                MaxLifeTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                AccelPerSec = 0f,
                DesiredSpeed = 300, // DO NOT SET HIGHER THAN 4100
                MaxTrajectory = 5000f,
                FieldTime = 0, // 0 is disabled, a value causes the projectile to come to rest, spawn a field and remain for a time (Measured in game ticks, 60 = 1 second)
                GravityMultiplier = 0f, // Gravity multiplier, influences the trajectory of the projectile, value greater than 0 to enable.
                SpeedVariance = Random(start: 0, end: 0), // subtracts value from DesiredSpeed
                RangeVariance = Random(start: 0, end: 0), // subtracts value from MaxTrajectory
                MaxTrajectoryTime = 0, // How long the weapon must fire before it reaches MaxTrajectory.
                Smarts = new SmartsDef
                {
                    Inaccuracy = 0f, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
                    Aggressiveness = 1f, // controls how responsive tracking is.
                    MaxLateralThrust = 0.5, // controls how sharp the trajectile may turn
                    TrackingDelay = 0, // Measured in Shape diameter units traveled.
                    MaxChaseTime = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    OverideTarget = true, // when set to true ammo picks its own target, does not use hardpoint's.
                    MaxTargets = 0, // Number of targets allowed before ending, 0 = unlimited
                    NoTargetExpire = false, // Expire without ever having a target at TargetLossTime
                    Roam = false, // Roam current area after target loss
                    KeepAliveAfterTargetLoss = false, // Whether to stop early death of projectile on target loss
                },
                Mines = new MinesDef
                {
                    DetectRadius = 0,
                    DeCloakRadius = 0,
                    FieldTime = 0,
                    Cloak = false,
                    Persist = false,
                },
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "",
                VisualProbability = 1f,
                ShieldHitDraw = false,
                Particles = new AmmoParticleDef
                {
                    Ammo = new ParticleDef
                    {
                        Name = "", //ShipWelderArc
                        ShrinkByDistance = false,
                        Color = Color(red: 128, green: 0, blue: 0, alpha: 32),
                        Offset = Vector(x: 0, y: -1, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 1,
                            Scale = 1,
                        },
                    },
                    Hit = new ParticleDef
                    {
                        Name = "",
                        ApplyToShield = true,
                        ShrinkByDistance = false,
                        Color = Color(red: 3, green: 1.9f, blue: 1f, alpha: 1),
                        Offset = Vector(x: 0, y: 0, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 30,
                            Scale = 1,
                            HitPlayChance = 1f,
                        },
                    },
                    Eject = new ParticleDef
                    {
                        Name = "",
                        ApplyToShield = true,
                        ShrinkByDistance = false,
                        Color = Color(red: 3, green: 1.9f, blue: 1f, alpha: 1),
                        Offset = Vector(x: 0, y: 0, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 30,
                            Scale = 1,
                            HitPlayChance = 1f,
                        },
                    },
                },
                Lines = new LineDef
                {
                    ColorVariance = Random(start: 0.75f, end: 2f), // multiply the color by random values within range.
                    WidthVariance = Random(start: 0f, end: 0f), // adds random value to default width (negatives shrinks width)
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = 1f,
                        Width = 0.2f,
                        Color = Color(red: 3, green: 2, blue: 1f, alpha: 1),
                        VisualFadeStart = 0, // Number of ticks the weapon has been firing before projectiles begin to fade their color
                        VisualFadeEnd = 0, // How many ticks after fade began before it will be invisible.
                        Textures = new[] {// WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                            "WeaponLaser",
                        },
                        TextureMode = Normal, // Normal, Cycle, Chaos, Wave
                        Segmentation = new SegmentDef
                        {
                            Enable = false, // If true Tracer TextureMode is ignored
                            Textures = new[] {
								"",
                            },
                            SegmentLength = 0f, // Uses the values below.
                            SegmentGap = 0f, // Uses Tracer textures and values
                            Speed = 1f, // meters per second
                            Color = Color(red: 1, green: 2, blue: 2.5f, alpha: 1),
                            WidthMultiplier = 1f,
                            Reverse = false,
                            UseLineVariance = true,
                            WidthVariance = Random(start: 0f, end: 0f),
                            ColorVariance = Random(start: 0f, end: 0f)
                        }
                    },
                    Trail = new TrailDef
                    {
                        Enable = false,
                        Textures = new[] {
							"",
                        },
                        TextureMode = Normal,
                        DecayTime = 128,
                        Color = Color(red: 0, green: 0, blue: 1, alpha: 1),
                        Back = false,
                        CustomWidth = 0,
                        UseWidthVariance = false,
                        UseColorFade = true,
                    },
                    OffsetEffect = new OffsetEffectDef
                    {
                        MaxOffset = 0,// 0 offset value disables this effect
                        MinLength = 0.2f,
                        MaxLength = 3,
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                TravelSound = "",
                HitSound = "",
                ShieldHitSound = "",
                PlayerHitSound = "",
                VoxelHitSound = "",
                FloatingHitSound = "",
                HitPlayChance = 0.5f,
                HitPlayShield = true,
            }, // Don't edit below this line
            Ejection = new EjectionDef
            {
                Type = Particle, // Particle or Item (Inventory Component)
                Speed = 100f, // Speed inventory is ejected from in dummy direction
                SpawnChance = 0.5f, // chance of triggering effect (0 - 1)
                CompDef = new ComponentDef
                {
                    ItemName = "", //InventoryComponent name
                    ItemLifeTime = 0, // how long item should exist in world
                    Delay = 0, // delay in ticks after shot before ejected
                }
            },
        };
		
        private AmmoDef Sigma400mmHE => new AmmoDef
        {
            
                AmmoMagazine = "Sigma400mmHE", //Name of your ammo type definied in your ammo.sbc file.
                AmmoRound = "400mm HE", //Desired model for the projectiles, must be defined in your ammo.sbc file.
                HybridRound = false, //AmmoMagazine based weapon with energy cost
                EnergyCost = 0.000001f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
                BaseDamage = 1f, //Think of this as level of penetration, low base damage for no penetration. Every Block destroyed takes their HP off the projectile.
                Mass = 20f, // in kilograms, determines the amount of kinetic energy applied to the grid being hit.
                Health = 1, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying. KEEP THIS LOW. 1=Standard,10=Insane
                BackKickForce = 1000f, //Determines the amount of kinetic energy applied to the grid firing.
				DecayPerShot = 0f,
				HardPointUsable = true, // set to false if this is a Fragment ammoType and you don't want the turret to be able to select it directly.
				EnergyMagazineSize = 12, //ONLY USE IF "AmmoMagazine=ENERGY". Otherwise specify magazine size in Ammo.sbc file.
				IgnoreWater = false,

                Shape = new ShapeDef //defines the collision shape of projectile, defaults line and visual Line Length if set to 0
                {
                    Shape = LineShape,
                    Diameter = 1,
                },
                ObjectsHit = new ObjectsHitDef
                {
                    MaxObjectsHit = 0, // 0 = disabled
                    CountBlocks = false, // counts gridBlocks and not just entities hit
                },
                Fragment = new FragmentDef
				{
					AmmoRound = "HEShrapnel",
					Fragments = 25, //Number of Fragment projectiles created on impact.
					Degrees = 180,  //Think of this as a cone in front of the projectile, 360 degrees for a full circle.
					Reverse = false, //Redirects the cone back towards the projectile's rear
					RandomizeDir = false,
				},
                DamageScales = new DamageScaleDef
                {
                    MaxIntegrity = 0f, // 0 = disabled, 1000 = any blocks with currently integrity above 1000 will be immune to damage.
                    DamageVoxels = false, // true = voxels are vulnerable to this weapon
                    SelfDamage = false, // true = allow self damage.
                    HealthHitModifier = 3, // defaults to a value of 1, this setting modifies how much Health is subtracted from a projectile per hit (1 = per hit).
                    VoxelHitModifier = 3,  // modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01 = 1% damage, 2 = 200% damage.
                    Characters = 0.2f, //Damage to characters. 1f=same as damage to grids
                    Grids = new GridSizeDef
                    {
                        Large = .5f,
                        Small = 1f,
                    },
                    Armor = new ArmorDef
                    {
                        Armor = .5f,
                        Light = -1f,
                        Heavy = -1f,
                        NonArmor = 1f,
                    },
                    Shields = new ShieldDef
                    {
                        Modifier = .5f,
                        Type = Default,
                        BypassModifier = -1f,
                    },
                    // first true/false (ignoreOthers) will cause projectiles to pass through all blocks that do not match the custom subtypeIds.
                    Custom = new CustomScalesDef
                    {
                        IgnoreAllOthers = false,
                        Types = new []
                        {
                            new CustomBlocksDef
                            {
                                SubTypeId = "Test1",
                                Modifier = -1f,
                            },
                            new CustomBlocksDef
                            {
                                SubTypeId = "Test2",
                                Modifier = -1f,
                            },
                        },
                    },
                },
                AreaEffect = new AreaDamageDef
                {
                    AreaEffect = Explosive, // Disabled = do not use area effect at all, Explosive, Radiant, AntiSmart, JumpNullField, JumpNullField, EnergySinkField, AnchorField, EmpField, OffenseField, NavField, DotField.
                    AreaEffectDamage = 0f, // 0 = DO NOT USE FOR EXPLOSIVES, USE DETONATION SETTINGS INSTEAD
                    AreaEffectRadius = 0f, // DO NOT USE FOR EXPLOSIVES, USE DETONATION SETTINGS INSTEAD
                    Pulse = new PulseDef // interval measured in game ticks (60 == 1 second), pulseChance chance (0 - 100) that an entity in field will be hit
                    {
                        Interval = 30,
                        PulseChance = 0,
                    },
                    Explosions = new ExplosionDef
                    {
                        NoVisuals = false, //Set to false to use vanilla explosion particles, unless specified a custom particle
                        NoSound = false, ////Set to false to use vanilla explosion noises, unless specified a custom sound
                        Scale = 2, //Scale of the particles.
                        CustomParticle = "",
                        CustomSound = "",
                    },
                    Detonation = new DetonateDef //USE THESE MODIFIERS FOR EXPLOSIONS
                    {
                        DetonateOnEnd = true,
                        ArmOnlyOnHit = false,
                        DetonationDamage = 500, //1 Steelplate =100HP, 1 large light armor cube=2500HP. Has a natural falloff based on radius.
                        DetonationRadius = 6, //Measured in meters, how large the explosion should be.
                    },
                    EwarFields = new EwarFieldsDef
                    {
                        Duration = 0,
                        StackDuration = true,
                        Depletable = false,
                        MaxStacks = 10,
                        TriggerRange = 5f,
                    },
                },
                Beams = new BeamDef
                {
                    Enable = false,
                    VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
                    ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
                    RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
                    OneParticle = false, // Only spawn one particle hit per beam weapon.
                },
                Trajectory = new TrajectoryDef
                {
                    Guidance = None,
                    TargetLossDegree = 180f,
                    TargetLossTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    MaxLifeTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    AccelPerSec = 0f,
                    DesiredSpeed = 100, //Speed of the projectile. DO NOT SET HIGHER THAN 4100.
                    MaxTrajectory = 5000f, //Maximum distance the projectile can travel before detonating/despawning.
                    FieldTime = 0, // 0 is disabled, a value causes the projectile to come to rest, spawn a field and remain for a time (Measured in game ticks, 60 = 1 second)
					GravityMultiplier = 1f, //1f=Gravity applies normal pull to projectile. 0 to disable.
                    SpeedVariance = Random(start: 0, end: 0), // subtracts value from DesiredSpeed
                    RangeVariance = Random(start: 0, end: 0), // subtracts value from MaxTrajectory
                    Smarts = new SmartsDef
                    {
                        Inaccuracy = 5f, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
                        Aggressiveness = 0.1f, // controls how responsive tracking is.
                        MaxLateralThrust = 0.05f, // controls how sharp the trajectile may turn
                        TrackingDelay = 400, // Measured in Shape diameter units traveled.
                        MaxChaseTime = 2000, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                        OverideTarget = true, // when set to true ammo picks its own target, does not use hardpoint's.
                    },
                    Mines = new MinesDef
                    {
                        DetectRadius =  200,
                        DeCloakRadius = 100,
                        FieldTime = 1800,
                        Cloak = true,
                        Persist = false,
                    },
                },
                AmmoGraphics = new GraphicDef
                {
                    ModelName = "\\Models\\Akiad\\Small\\Sigma11Rocket.mwm", //USE DOUBLE LINES, ex: "\\Models\\Name\\Small\\MyGun.mwm"
                    VisualProbability = 1f, //Percentage chance for the model to spawn, "1f" for 100% chance.
                    ShieldHitDraw = true,
                    Particles = new AmmoParticleDef
                    {
                        Ammo = new ParticleDef
                        {
                            Name = "Smoke_Missile", //Particle that surrounds your projectile. ShipWelderArc
                            Color = Color(red: 1f, green: 1f, blue: 1f, alpha: 1),
                            Offset = Vector(x: 0, y: 0, z: 0),
                            Extras = new ParticleOptionDef
                            {
                                Loop = true,
                                Restart = false,
                                MaxDistance = 5000,
                                MaxDuration = 1,
                                Scale = 0.25f,
                            },
                        },
                        Hit = new ParticleDef
                        {
                            Name = "",
                            ApplyToShield = true,
                            ShrinkByDistance = true,
                            Color = Color(red: 3, green: 2, blue: 1, alpha: 1),
                            Offset = Vector(x: 0, y: 0, z: 0),
                            Extras = new ParticleOptionDef
                            {
                                Loop = false,
                                Restart = false,
                                MaxDistance = 500,
                                MaxDuration = 1,
                                Scale = 1,
                                HitPlayChance = 1f,
                            },
                        },
                    },
                    Lines = new LineDef
                    {
                        TracerMaterial = "ProjectileTrailLine", // Particle that follows your projectile. WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                        ColorVariance = Random(start: 5f, end: 10f), // multiply the color by random values within range.
                        WidthVariance = Random(start: 0f, end: 0.045f), // adds random value to default width (negatives shrinks width)
                        Tracer = new TracerBaseDef
                        {
                            Enable = false,
                            Length = 3f,
                            Width = 0.05f,
                            Color = Color(red: 0.9f, green: 0.9f, blue: 0.9f, alpha: 1),
                        },
                        Trail = new TrailDef
                        {
                            Enable = false,
                            Material = "WeaponLaser",
                            DecayTime = 128,
                            Color = Color(red: 0, green: 0, blue: 1, alpha: 1),
                            Back = true,
                            CustomWidth = 0,
                            UseWidthVariance = false,
                            UseColorFade = true,
                        },
                        OffsetEffect = new OffsetEffectDef
                        {
                            MaxOffset =  0,// 0 offset value disables this effect
                            MinLength = 0.2f,
                            MaxLength = 3,
                        },
                    },
                },
                AmmoAudio = new AmmoAudioDef
                {
                    TravelSound = "",
                    HitSound = "ArcImpMetalMetalCat0",
                    HitPlayChance = 0.1f,
                    HitPlayShield = true,
                }, // Don't edit below this line

        };
		
		private AmmoDef JSTRRocket => new AmmoDef
        {
            
                AmmoMagazine = "JSTRRocket", //Name of your ammo type definied in your ammo.sbc file.
                AmmoRound = "JSTRRocket", //Desired model for the projectiles, must be defined in your ammo.sbc file.
                HybridRound = false, //AmmoMagazine based weapon with energy cost
                EnergyCost = 0.00000000001f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
                BaseDamage = 12f, //Think of this as level of penetration, low base damage for no penetration. Every Block destroyed takes their HP off the projectile.
                Mass = 20f, // in kilograms, determines the amount of kinetic energy applied to the grid being hit.
                Health = 1, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying. KEEP THIS LOW. 1=Standard,10=Insane
                BackKickForce = 1000f, //Determines the amount of kinetic energy applied to the grid firing.
				DecayPerShot = 0f,
				HardPointUsable = true, // set to false if this is a Fragment ammoType and you don't want the turret to be able to select it directly.
				EnergyMagazineSize = 12, //ONLY USE IF "AmmoMagazine=ENERGY". Otherwise specify magazine size in Ammo.sbc file.
				IgnoreWater = false,

                Shape = new ShapeDef //defines the collision shape of projectile, defaults line and visual Line Length if set to 0
                {
                    Shape = LineShape,
                    Diameter = 1,
                },
                ObjectsHit = new ObjectsHitDef
                {
                    MaxObjectsHit = 0, // 0 = disabled
                    CountBlocks = false, // counts gridBlocks and not just entities hit
                },
                Fragment = new FragmentDef
				{
					AmmoRound = "HEShrapnel",
					Fragments = 25, //Number of Fragment projectiles created on impact.
					Degrees = 180,  //Think of this as a cone in front of the projectile, 360 degrees for a full circle.
					Reverse = false, //Redirects the cone back towards the projectile's rear
					RandomizeDir = false,
				},
                DamageScales = new DamageScaleDef
                {
                    MaxIntegrity = 0f, // 0 = disabled, 1000 = any blocks with currently integrity above 1000 will be immune to damage.
                    DamageVoxels = false, // true = voxels are vulnerable to this weapon
                    SelfDamage = false, // true = allow self damage.
                    HealthHitModifier = 3, // defaults to a value of 1, this setting modifies how much Health is subtracted from a projectile per hit (1 = per hit).
                    VoxelHitModifier = 3,  // modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01 = 1% damage, 2 = 200% damage.
                    Characters = 0.2f, //Damage to characters. 1f=same as damage to grids
                    Grids = new GridSizeDef
                    {
                        Large = .5f,
                        Small = 1f,
                    },
                    Armor = new ArmorDef
                    {
                        Armor = .5f,
                        Light = -1f,
                        Heavy = -1f,
                        NonArmor = 1f,
                    },
                    Shields = new ShieldDef
                    {
                        Modifier = .5f,
                        Type = Default,
                        BypassModifier = -1f,
                    },
                    // first true/false (ignoreOthers) will cause projectiles to pass through all blocks that do not match the custom subtypeIds.
                    Custom = new CustomScalesDef
                    {
                        IgnoreAllOthers = false,
                        Types = new []
                        {
                            new CustomBlocksDef
                            {
                                SubTypeId = "Test1",
                                Modifier = -1f,
                            },
                            new CustomBlocksDef
                            {
                                SubTypeId = "Test2",
                                Modifier = -1f,
                            },
                        },
                    },
                },
                AreaEffect = new AreaDamageDef
                {
                    AreaEffect = Explosive, // Disabled = do not use area effect at all, Explosive, Radiant, AntiSmart, JumpNullField, JumpNullField, EnergySinkField, AnchorField, EmpField, OffenseField, NavField, DotField.
                    AreaEffectDamage = 0f, // 0 = DO NOT USE FOR EXPLOSIVES, USE DETONATION SETTINGS INSTEAD
                    AreaEffectRadius = 0f, // DO NOT USE FOR EXPLOSIVES, USE DETONATION SETTINGS INSTEAD
                    Pulse = new PulseDef // interval measured in game ticks (60 == 1 second), pulseChance chance (0 - 100) that an entity in field will be hit
                    {
                        Interval = 30,
                        PulseChance = 0,
                    },
                    Explosions = new ExplosionDef
                    {
                        NoVisuals = false, //Set to false to use vanilla explosion particles, unless specified a custom particle
                        NoSound = false, ////Set to false to use vanilla explosion noises, unless specified a custom sound
                        Scale = 2, //Scale of the particles.
                        CustomParticle = "",
                        CustomSound = "",
                    },
                    Detonation = new DetonateDef //USE THESE MODIFIERS FOR EXPLOSIONS
                    {
                        DetonateOnEnd = true,
                        ArmOnlyOnHit = false,
                        DetonationDamage = 3000, //1 Steelplate =100HP, 1 large light armor cube=2500HP. Has a natural falloff based on radius.
                        DetonationRadius = 6, //Measured in meters, how large the explosion should be.
                    },
                    EwarFields = new EwarFieldsDef
                    {
                        Duration = 0,
                        StackDuration = true,
                        Depletable = false,
                        MaxStacks = 10,
                        TriggerRange = 5f,
                    },
                },
                Beams = new BeamDef
                {
                    Enable = false,
                    VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
                    ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
                    RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
                    OneParticle = false, // Only spawn one particle hit per beam weapon.
                },
                Trajectory = new TrajectoryDef
                {
                    Guidance = None,
                    TargetLossDegree = 180f,
                    TargetLossTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    MaxLifeTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    AccelPerSec = 0f,
                    DesiredSpeed = 600, //Speed of the projectile. DO NOT SET HIGHER THAN 4100.
                    MaxTrajectory = 5000f, //Maximum distance the projectile can travel before detonating/despawning.
                    FieldTime = 0, // 0 is disabled, a value causes the projectile to come to rest, spawn a field and remain for a time (Measured in game ticks, 60 = 1 second)
					GravityMultiplier = 1f, //1f=Gravity applies normal pull to projectile. 0 to disable.
                    SpeedVariance = Random(start: 0, end: 0), // subtracts value from DesiredSpeed
                    RangeVariance = Random(start: 0, end: 0), // subtracts value from MaxTrajectory
                    Smarts = new SmartsDef
                    {
                        Inaccuracy = 5f, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
                        Aggressiveness = 0.1f, // controls how responsive tracking is.
                        MaxLateralThrust = 0.05f, // controls how sharp the trajectile may turn
                        TrackingDelay = 400, // Measured in Shape diameter units traveled.
                        MaxChaseTime = 2000, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                        OverideTarget = true, // when set to true ammo picks its own target, does not use hardpoint's.
                    },
                    Mines = new MinesDef
                    {
                        DetectRadius =  200,
                        DeCloakRadius = 100,
                        FieldTime = 1800,
                        Cloak = true,
                        Persist = false,
                    },
                },
                AmmoGraphics = new GraphicDef
                {
                    ModelName = "\\Models\\Akiad\\Small\\JSTRRocket.mwm", //USE DOUBLE LINES, ex: "\\Models\\Name\\Small\\MyGun.mwm"
                    VisualProbability = 1f, //Percentage chance for the model to spawn, "1f" for 100% chance.
                    ShieldHitDraw = true,
                    Particles = new AmmoParticleDef
                    {
                        Ammo = new ParticleDef
                        {
                            Name = "Smoke_Missile", //Particle that surrounds your projectile. ShipWelderArc
                            Color = Color(red: 1f, green: 1f, blue: 1f, alpha: 1),
                            Offset = Vector(x: 0, y: 0, z: 0),
                            Extras = new ParticleOptionDef
                            {
                                Loop = true,
                                Restart = false,
                                MaxDistance = 5000,
                                MaxDuration = 1,
                                Scale = 0.25f,
                            },
                        },
                        Hit = new ParticleDef
                        {
                            Name = "",
                            ApplyToShield = true,
                            ShrinkByDistance = true,
                            Color = Color(red: 3, green: 2, blue: 1, alpha: 1),
                            Offset = Vector(x: 0, y: 0, z: 0),
                            Extras = new ParticleOptionDef
                            {
                                Loop = false,
                                Restart = false,
                                MaxDistance = 500,
                                MaxDuration = 1,
                                Scale = 1,
                                HitPlayChance = 1f,
                            },
                        },
                    },
                    Lines = new LineDef
                    {
                        TracerMaterial = "ProjectileTrailLine", // Particle that follows your projectile. WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                        ColorVariance = Random(start: 5f, end: 10f), // multiply the color by random values within range.
                        WidthVariance = Random(start: 0f, end: 0.045f), // adds random value to default width (negatives shrinks width)
                        Tracer = new TracerBaseDef
                        {
                            Enable = true,
                            Length = 3f,
                            Width = 0.05f,
                            Color = Color(red: 0.9f, green: 0.9f, blue: 0.9f, alpha: 1),
                        },
                        Trail = new TrailDef
                        {
                            Enable = false,
                            Material = "WeaponLaser",
                            DecayTime = 10,
                            Color = Color(red: 10, green: 1, blue: 1, alpha: 1),
                            Back = true,
                            CustomWidth = 0,
                            UseWidthVariance = false,
                            UseColorFade = true,
                        },
                        OffsetEffect = new OffsetEffectDef
                        {
                            MaxOffset =  0,// 0 offset value disables this effect
                            MinLength = 0.2f,
                            MaxLength = 3,
                        },
                    },
                },
                AmmoAudio = new AmmoAudioDef
                {
                    TravelSound = "",
                    HitSound = "ArcImpMetalMetalCat0",
                    HitPlayChance = 0.1f,
                    HitPlayShield = true,
                }, // Don't edit below this line

        };
		
		private AmmoDef Ares600mmHE => new AmmoDef
        {
            
                AmmoMagazine = "Ares600mmHE", //Name of your ammo type definied in your ammo.sbc file.
                AmmoRound = "400mm HE", //Desired model for the projectiles, must be defined in your ammo.sbc file.
                HybridRound = false, //AmmoMagazine based weapon with energy cost
                EnergyCost = 0.00000000001f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
                BaseDamage = 1f, //Think of this as level of penetration, low base damage for no penetration. Every Block destroyed takes their HP off the projectile.
                Mass = 20f, // in kilograms, determines the amount of kinetic energy applied to the grid being hit.
                Health = 1, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying. KEEP THIS LOW. 1=Standard,10=Insane
                BackKickForce = 1000f, //Determines the amount of kinetic energy applied to the grid firing.
				DecayPerShot = 0f,
				HardPointUsable = true, // set to false if this is a Fragment ammoType and you don't want the turret to be able to select it directly.
				EnergyMagazineSize = 12, //ONLY USE IF "AmmoMagazine=ENERGY". Otherwise specify magazine size in Ammo.sbc file.
				IgnoreWater = false,

                Shape = new ShapeDef //defines the collision shape of projectile, defaults line and visual Line Length if set to 0
                {
                    Shape = LineShape,
                    Diameter = 1,
                },
                ObjectsHit = new ObjectsHitDef
                {
                    MaxObjectsHit = 0, // 0 = disabled
                    CountBlocks = false, // counts gridBlocks and not just entities hit
                },
                Fragment = new FragmentDef
				{
					AmmoRound = "",
					Fragments = 25, //Number of Fragment projectiles created on impact.
					Degrees = 180,  //Think of this as a cone in front of the projectile, 360 degrees for a full circle.
					Reverse = false, //Redirects the cone back towards the projectile's rear
					RandomizeDir = false,
				},
                DamageScales = new DamageScaleDef
                {
                    MaxIntegrity = 0f, // 0 = disabled, 1000 = any blocks with currently integrity above 1000 will be immune to damage.
                    DamageVoxels = false, // true = voxels are vulnerable to this weapon
                    SelfDamage = false, // true = allow self damage.
                    HealthHitModifier = 2, // defaults to a value of 1, this setting modifies how much Health is subtracted from a projectile per hit (1 = per hit).
                    VoxelHitModifier = 3,  // modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01 = 1% damage, 2 = 200% damage.
                    Characters = 0.2f, //Damage to characters. 1f=same as damage to grids
                    Grids = new GridSizeDef
                    {
                        Large = .5f,
                        Small = 1f,
                    },
                    Armor = new ArmorDef
                    {
                        Armor = .5f,
                        Light = -1f,
                        Heavy = -1f,
                        NonArmor = 1f,
                    },
                    Shields = new ShieldDef
                    {
                        Modifier = .5f,
                        Type = Default,
                        BypassModifier = -1f,
                    },
                    // first true/false (ignoreOthers) will cause projectiles to pass through all blocks that do not match the custom subtypeIds.
                    Custom = new CustomScalesDef
                    {
                        IgnoreAllOthers = false,
                        Types = new []
                        {
                            new CustomBlocksDef
                            {
                                SubTypeId = "Test1",
                                Modifier = -1f,
                            },
                            new CustomBlocksDef
                            {
                                SubTypeId = "Test2",
                                Modifier = -1f,
                            },
                        },
                    },
                },
                AreaEffect = new AreaDamageDef
                {
                    AreaEffect = Explosive, // Disabled = do not use area effect at all, Explosive, Radiant, AntiSmart, JumpNullField, JumpNullField, EnergySinkField, AnchorField, EmpField, OffenseField, NavField, DotField.
                    AreaEffectDamage = 0f, // 0 = DO NOT USE FOR EXPLOSIVES, USE DETONATION SETTINGS INSTEAD
                    AreaEffectRadius = 0f, // DO NOT USE FOR EXPLOSIVES, USE DETONATION SETTINGS INSTEAD
                    Pulse = new PulseDef // interval measured in game ticks (60 == 1 second), pulseChance chance (0 - 100) that an entity in field will be hit
                    {
                        Interval = 30,
                        PulseChance = 0,
                    },
                    Explosions = new ExplosionDef
                    {
                        NoVisuals = false, //Set to false to use vanilla explosion particles, unless specified a custom particle
                        NoSound = false, ////Set to false to use vanilla explosion noises, unless specified a custom sound
                        Scale = 2, //Scale of the particles.
                        CustomParticle = "",
                        CustomSound = "",
                    },
                    Detonation = new DetonateDef //USE THESE MODIFIERS FOR EXPLOSIONS
                    {
                        DetonateOnEnd = true,
                        ArmOnlyOnHit = false,
                        DetonationDamage = 1000, //1 Steelplate =100HP, 1 large light armor cube=2500HP. Has a natural falloff based on radius.
                        DetonationRadius = 10, //Measured in meters, how large the explosion should be.
                    },
                    EwarFields = new EwarFieldsDef
                    {
                        Duration = 0,
                        StackDuration = true,
                        Depletable = false,
                        MaxStacks = 10,
                        TriggerRange = 5f,
                    },
                },
                Beams = new BeamDef
                {
                    Enable = false,
                    VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
                    ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
                    RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
                    OneParticle = false, // Only spawn one particle hit per beam weapon.
                },
                Trajectory = new TrajectoryDef
                {
                    Guidance = None,
                    TargetLossDegree = 180f,
                    TargetLossTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    MaxLifeTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    AccelPerSec = 0f,
                    DesiredSpeed = 100, //Speed of the projectile. DO NOT SET HIGHER THAN 4100.
                    MaxTrajectory = 5000f, //Maximum distance the projectile can travel before detonating/despawning.
                    FieldTime = 0, // 0 is disabled, a value causes the projectile to come to rest, spawn a field and remain for a time (Measured in game ticks, 60 = 1 second)
					GravityMultiplier = 1f, //1f=Gravity applies normal pull to projectile. 0 to disable.
                    SpeedVariance = Random(start: 0, end: 0), // subtracts value from DesiredSpeed
                    RangeVariance = Random(start: 0, end: 0), // subtracts value from MaxTrajectory
                    Smarts = new SmartsDef
                    {
                        Inaccuracy = 5f, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
                        Aggressiveness = 0.1f, // controls how responsive tracking is.
                        MaxLateralThrust = 0.05f, // controls how sharp the trajectile may turn
                        TrackingDelay = 400, // Measured in Shape diameter units traveled.
                        MaxChaseTime = 2000, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                        OverideTarget = false, // when set to true ammo picks its own target, does not use hardpoint's.
                    },
                    Mines = new MinesDef
                    {
                        DetectRadius =  200,
                        DeCloakRadius = 100,
                        FieldTime = 1800,
                        Cloak = true,
                        Persist = false,
                    },
                },
                AmmoGraphics = new GraphicDef
                {
                    ModelName = "\\Models\\Akiad\\Small\\AresRocket.mwm", //USE DOUBLE LINES, ex: "\\Models\\Name\\Small\\MyGun.mwm"
                    VisualProbability = 1f, //Percentage chance for the model to spawn, "1f" for 100% chance.
                    ShieldHitDraw = true,
                    Particles = new AmmoParticleDef
                    {
                        Ammo = new ParticleDef
                        {
                            Name = "MyMissile", //Particle that surrounds your projectile. ShipWelderArc
                            Color = Color(red: 1f, green: 1f, blue: 1f, alpha: 1),
                            Offset = Vector(x: 0, y: 0, z: 0),
                            Extras = new ParticleOptionDef
                            {
                                Loop = true,
                                Restart = false,
                                MaxDistance = 5000,
                                MaxDuration = 1,
                                Scale = 0.25f,
                            },
                        },
                        Hit = new ParticleDef
                        {
                            Name = "",
                            ApplyToShield = true,
                            ShrinkByDistance = true,
                            Color = Color(red: 3, green: 2, blue: 1, alpha: 1),
                            Offset = Vector(x: 0, y: 0, z: 0),
                            Extras = new ParticleOptionDef
                            {
                                Loop = false,
                                Restart = false,
                                MaxDistance = 500,
                                MaxDuration = 1,
                                Scale = 1,
                                HitPlayChance = 1f,
                            },
                        },
                    },
                    Lines = new LineDef
                    {
                        TracerMaterial = "ProjectileTrailLine", // Particle that follows your projectile. WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                        ColorVariance = Random(start: 5f, end: 10f), // multiply the color by random values within range.
                        WidthVariance = Random(start: 0f, end: 0.045f), // adds random value to default width (negatives shrinks width)
                        Tracer = new TracerBaseDef
                        {
                            Enable = false,
                            Length = 3f,
                            Width = 0.05f,
                            Color = Color(red: 0.9f, green: 0.9f, blue: 0.9f, alpha: 1),
                        },
                        Trail = new TrailDef
                        {
                            Enable = false,
                            Material = "WeaponLaser",
                            DecayTime = 128,
                            Color = Color(red: 0, green: 0, blue: 1, alpha: 1),
                            Back = true,
                            CustomWidth = 0,
                            UseWidthVariance = false,
                            UseColorFade = true,
                        },
                        OffsetEffect = new OffsetEffectDef
                        {
                            MaxOffset =  0,// 0 offset value disables this effect
                            MinLength = 0.2f,
                            MaxLength = 3,
                        },
                    },
                },
                AmmoAudio = new AmmoAudioDef
                {
                    TravelSound = "",
                    HitSound = "ArcImpMetalMetalCat0",
                    HitPlayChance = 0.1f,
                    HitPlayShield = true,
                }, // Don't edit below this line

        };
		
		private AmmoDef Kraken800mm => new AmmoDef
        {
            
                AmmoMagazine = "Kraken800mm", //Name of your ammo type definied in your ammo.sbc file.
                AmmoRound = "188mm HE", //Desired model for the projectiles, must be defined in your ammo.sbc file.
                HybridRound = false, //AmmoMagazine based weapon with energy cost
                EnergyCost = 0.00000000001f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
                BaseDamage = 1f, //Think of this as level of penetration, low base damage for no penetration. Every Block destroyed takes their HP off the projectile.
                Mass = 20f, // in kilograms, determines the amount of kinetic energy applied to the grid being hit.
                Health = 1, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying. KEEP THIS LOW. 1=Standard,10=Insane
                BackKickForce = 1000f, //Determines the amount of kinetic energy applied to the grid firing.
				DecayPerShot = 0f,
				HardPointUsable = true, // set to false if this is a Fragment ammoType and you don't want the turret to be able to select it directly.
				EnergyMagazineSize = 12, //ONLY USE IF "AmmoMagazine=ENERGY". Otherwise specify magazine size in Ammo.sbc file.
				IgnoreWater = false,

                Shape = new ShapeDef //defines the collision shape of projectile, defaults line and visual Line Length if set to 0
                {
                    Shape = LineShape,
                    Diameter = 1,
                },
                ObjectsHit = new ObjectsHitDef
                {
                    MaxObjectsHit = 0, // 0 = disabled
                    CountBlocks = false, // counts gridBlocks and not just entities hit
                },
                Fragment = new FragmentDef
				{
					AmmoRound = "KrakenStage2",
					Fragments = 1, //Number of Fragment projectiles created on impact.
					Degrees = 1,  //Think of this as a cone in front of the projectile, 360 degrees for a full circle.
					Reverse = false, //Redirects the cone back towards the projectile's rear
					RandomizeDir = false,
				},
                DamageScales = new DamageScaleDef
                {
                    MaxIntegrity = 0f, // 0 = disabled, 1000 = any blocks with currently integrity above 1000 will be immune to damage.
                    DamageVoxels = false, // true = voxels are vulnerable to this weapon
                    SelfDamage = false, // true = allow self damage.
                    HealthHitModifier = 2, // defaults to a value of 1, this setting modifies how much Health is subtracted from a projectile per hit (1 = per hit).
                    VoxelHitModifier = 3,  // modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01 = 1% damage, 2 = 200% damage.
                    Characters = 0.2f, //Damage to characters. 1f=same as damage to grids
                    Grids = new GridSizeDef
                    {
                        Large = .5f,
                        Small = 1f,
                    },
                    Armor = new ArmorDef
                    {
                        Armor = .5f,
                        Light = -1f,
                        Heavy = -1f,
                        NonArmor = 1f,
                    },
                    Shields = new ShieldDef
                    {
                        Modifier = .5f,
                        Type = Default,
                        BypassModifier = -1f,
                    },
                    // first true/false (ignoreOthers) will cause projectiles to pass through all blocks that do not match the custom subtypeIds.
                    Custom = new CustomScalesDef
                    {
                        IgnoreAllOthers = false,
                        Types = new []
                        {
                            new CustomBlocksDef
                            {
                                SubTypeId = "Test1",
                                Modifier = -1f,
                            },
                            new CustomBlocksDef
                            {
                                SubTypeId = "Test2",
                                Modifier = -1f,
                            },
                        },
                    },
                },
                AreaEffect = new AreaDamageDef
                {
                    AreaEffect = Explosive, // Disabled = do not use area effect at all, Explosive, Radiant, AntiSmart, JumpNullField, JumpNullField, EnergySinkField, AnchorField, EmpField, OffenseField, NavField, DotField.
                    AreaEffectDamage = 0f, // 0 = DO NOT USE FOR EXPLOSIVES, USE DETONATION SETTINGS INSTEAD
                    AreaEffectRadius = 0f, // DO NOT USE FOR EXPLOSIVES, USE DETONATION SETTINGS INSTEAD
                    Pulse = new PulseDef // interval measured in game ticks (60 == 1 second), pulseChance chance (0 - 100) that an entity in field will be hit
                    {
                        Interval = 30,
                        PulseChance = 0,
                    },
                    Explosions = new ExplosionDef
                    {
                        NoVisuals = true, //Set to false to use vanilla explosion particles, unless specified a custom particle
                        NoSound = true, ////Set to false to use vanilla explosion noises, unless specified a custom sound
                        Scale = 2, //Scale of the particles.
                        CustomParticle = "",
                        CustomSound = "",
                    },
                    Detonation = new DetonateDef //USE THESE MODIFIERS FOR EXPLOSIONS
                    {
                        DetonateOnEnd = true,
                        ArmOnlyOnHit = false,
                        DetonationDamage = 0, //1 Steelplate =100HP, 1 large light armor cube=2500HP. Has a natural falloff based on radius.
                        DetonationRadius = 15, //Measured in meters, how large the explosion should be.
                    },
                    EwarFields = new EwarFieldsDef
                    {
                        Duration = 0,
                        StackDuration = true,
                        Depletable = false,
                        MaxStacks = 10,
                        TriggerRange = 5f,
                    },
                },
                Beams = new BeamDef
                {
                    Enable = false,
                    VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
                    ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
                    RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
                    OneParticle = false, // Only spawn one particle hit per beam weapon.
                },
                Trajectory = new TrajectoryDef
                {
                    Guidance = Smart,
                    TargetLossDegree = 180f,
                    TargetLossTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    MaxLifeTime = 60, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    AccelPerSec = 0f,
                    DesiredSpeed = 10, //Speed of the projectile. DO NOT SET HIGHER THAN 4100.
                    MaxTrajectory = 5000f, //Maximum distance the projectile can travel before detonating/despawning.
                    FieldTime = 0, // 0 is disabled, a value causes the projectile to come to rest, spawn a field and remain for a time (Measured in game ticks, 60 = 1 second)
					GravityMultiplier = 0f, //1f=Gravity applies normal pull to projectile. 0 to disable.
                    SpeedVariance = Random(start: 0, end: 0), // subtracts value from DesiredSpeed
                    RangeVariance = Random(start: 0, end: 0), // subtracts value from MaxTrajectory
                    Smarts = new SmartsDef
                    {
                        Inaccuracy = 0, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
                        Aggressiveness = 3f, // controls how responsive tracking is.
                        MaxLateralThrust = .5f, // controls how sharp the trajectile may turn
                        TrackingDelay = 0, // Measured in Shape diameter units traveled.
                        MaxChaseTime = 2000, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                        OverideTarget = false, // when set to true ammo picks its own target, does not use hardpoint's.
                    },
                    Mines = new MinesDef
                    {
                        DetectRadius =  200,
                        DeCloakRadius = 100,
                        FieldTime = 1800,
                        Cloak = true,
                        Persist = false,
                    },
                },
                AmmoGraphics = new GraphicDef
                {
                    ModelName = "\\Models\\Akiad\\Small\\KrakenMissile.mwm", //USE DOUBLE LINES, ex: "\\Models\\Name\\Small\\MyGun.mwm"
                    VisualProbability = 1f, //Percentage chance for the model to spawn, "1f" for 100% chance.
                    ShieldHitDraw = true,
                    Particles = new AmmoParticleDef
                    {
                        Ammo = new ParticleDef
                        {
                            Name = "", //Particle that surrounds your projectile. ShipWelderArc
                            Color = Color(red: 1f, green: 1f, blue: 1f, alpha: 1),
                            Offset = Vector(x: 0, y: 0, z: 0),
                            Extras = new ParticleOptionDef
                            {
                                Loop = true,
                                Restart = false,
                                MaxDistance = 5000,
                                MaxDuration = 1,
                                Scale = 0.25f,
                            },
                        },
                        Hit = new ParticleDef
                        {
                            Name = "",
                            ApplyToShield = true,
                            ShrinkByDistance = true,
                            Color = Color(red: 3, green: 2, blue: 1, alpha: 1),
                            Offset = Vector(x: 0, y: 0, z: 0),
                            Extras = new ParticleOptionDef
                            {
                                Loop = false,
                                Restart = false,
                                MaxDistance = 500,
                                MaxDuration = 1,
                                Scale = 1,
                                HitPlayChance = 1f,
                            },
                        },
                    },
                    Lines = new LineDef
                    {
                        TracerMaterial = "ProjectileTrailLine", // Particle that follows your projectile. WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                        ColorVariance = Random(start: 5f, end: 10f), // multiply the color by random values within range.
                        WidthVariance = Random(start: 0f, end: 0.045f), // adds random value to default width (negatives shrinks width)
                        Tracer = new TracerBaseDef
                        {
                            Enable = true,
                            Length = .001f,
                            Width = .001f,
                            Color = Color(red: 0.9f, green: 0.9f, blue: 20f, alpha: 1),
                        },
                        Trail = new TrailDef
                        {
                            Enable = true,
                            Material = "WeaponLaser",
                            DecayTime = 10,
                            Color = Color(red: 20, green: 20, blue: 100, alpha: 1),
                            Back = true,
                            CustomWidth = .05f,
                            UseWidthVariance = false,
                            UseColorFade = true,
                        },
                        OffsetEffect = new OffsetEffectDef
                        {
                            MaxOffset =  0,// 0 offset value disables this effect
                            MinLength = 0.2f,
                            MaxLength = 3,
                        },
                    },
                },
                AmmoAudio = new AmmoAudioDef
                {
                    TravelSound = "",
                    HitSound = "ArcImpMetalMetalCat0",
                    HitPlayChance = 0.1f,
                    HitPlayShield = true,
                }, // Don't edit below this line

        };
		
		private AmmoDef KrakenStage2 => new AmmoDef
        {
            
                AmmoMagazine = "KrakenStage2", //Name of your ammo type definied in your ammo.sbc file.
                AmmoRound = "KrakenStage2", //Desired model for the projectiles, must be defined in your ammo.sbc file.
                HybridRound = false, //AmmoMagazine based weapon with energy cost
                EnergyCost = 0.00000000001f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
                BaseDamage = 1f, //Think of this as level of penetration, low base damage for no penetration. Every Block destroyed takes their HP off the projectile.
                Mass = 20f, // in kilograms, determines the amount of kinetic energy applied to the grid being hit.
                Health = 1, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying. KEEP THIS LOW. 1=Standard,10=Insane
                BackKickForce = 1000f, //Determines the amount of kinetic energy applied to the grid firing.
				DecayPerShot = 0f,
				HardPointUsable = false, // set to false if this is a Fragment ammoType and you don't want the turret to be able to select it directly.
				EnergyMagazineSize = 12, //ONLY USE IF "AmmoMagazine=ENERGY". Otherwise specify magazine size in Ammo.sbc file.
				IgnoreWater = false,

                Shape = new ShapeDef //defines the collision shape of projectile, defaults line and visual Line Length if set to 0
                {
                    Shape = LineShape,
                    Diameter = 1,
                },
                ObjectsHit = new ObjectsHitDef
                {
                    MaxObjectsHit = 0, // 0 = disabled
                    CountBlocks = false, // counts gridBlocks and not just entities hit
                },
                Fragment = new FragmentDef
				{
					AmmoRound = "",
					Fragments = 25, //Number of Fragment projectiles created on impact.
					Degrees = 180,  //Think of this as a cone in front of the projectile, 360 degrees for a full circle.
					Reverse = false, //Redirects the cone back towards the projectile's rear
					RandomizeDir = false,
				},
                DamageScales = new DamageScaleDef
                {
                    MaxIntegrity = 0f, // 0 = disabled, 1000 = any blocks with currently integrity above 1000 will be immune to damage.
                    DamageVoxels = false, // true = voxels are vulnerable to this weapon
                    SelfDamage = false, // true = allow self damage.
                    HealthHitModifier = 1, // defaults to a value of 1, this setting modifies how much Health is subtracted from a projectile per hit (1 = per hit).
                    VoxelHitModifier = 3,  // modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01 = 1% damage, 2 = 200% damage.
                    Characters = 0.2f, //Damage to characters. 1f=same as damage to grids
                    Grids = new GridSizeDef
                    {
                        Large = .5f,
                        Small = 1f,
                    },
                    Armor = new ArmorDef
                    {
                        Armor = .5f,
                        Light = -1f,
                        Heavy = -1f,
                        NonArmor = 1f,
                    },
                    Shields = new ShieldDef
                    {
                        Modifier = .5f,
                        Type = Default,
                        BypassModifier = -1f,
                    },
                    // first true/false (ignoreOthers) will cause projectiles to pass through all blocks that do not match the custom subtypeIds.
                    Custom = new CustomScalesDef
                    {
                        IgnoreAllOthers = false,
                        Types = new []
                        {
                            new CustomBlocksDef
                            {
                                SubTypeId = "Test1",
                                Modifier = -1f,
                            },
                            new CustomBlocksDef
                            {
                                SubTypeId = "Test2",
                                Modifier = -1f,
                            },
                        },
                    },
                },
                AreaEffect = new AreaDamageDef
                {
                    AreaEffect = Explosive, // Disabled = do not use area effect at all, Explosive, Radiant, AntiSmart, JumpNullField, JumpNullField, EnergySinkField, AnchorField, EmpField, OffenseField, NavField, DotField.
                    AreaEffectDamage = 0f, // 0 = DO NOT USE FOR EXPLOSIVES, USE DETONATION SETTINGS INSTEAD
                    AreaEffectRadius = 0f, // DO NOT USE FOR EXPLOSIVES, USE DETONATION SETTINGS INSTEAD
                    Pulse = new PulseDef // interval measured in game ticks (60 == 1 second), pulseChance chance (0 - 100) that an entity in field will be hit
                    {
                        Interval = 30,
                        PulseChance = 0,
                    },
                    Explosions = new ExplosionDef
                    {
                        NoVisuals = false, //Set to false to use vanilla explosion particles, unless specified a custom particle
                        NoSound = false, ////Set to false to use vanilla explosion noises, unless specified a custom sound
                        Scale = 2, //Scale of the particles.
                        CustomParticle = "",
                        CustomSound = "",
                    },
                    Detonation = new DetonateDef //USE THESE MODIFIERS FOR EXPLOSIONS
                    {
                        DetonateOnEnd = true,
                        ArmOnlyOnHit = false,
                        DetonationDamage = 1500, //1 Steelplate =100HP, 1 large light armor cube=2500HP. Has a natural falloff based on radius.
                        DetonationRadius = 15, //Measured in meters, how large the explosion should be.
                    },
                    EwarFields = new EwarFieldsDef
                    {
                        Duration = 0,
                        StackDuration = true,
                        Depletable = false,
                        MaxStacks = 10,
                        TriggerRange = 5f,
                    },
                },
                Beams = new BeamDef
                {
                    Enable = false,
                    VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
                    ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
                    RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
                    OneParticle = false, // Only spawn one particle hit per beam weapon.
                },
                Trajectory = new TrajectoryDef
                {
                    Guidance = Smart,
                    TargetLossDegree = 180f,
                    TargetLossTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    MaxLifeTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    AccelPerSec = 100f,
                    DesiredSpeed = 300, //Speed of the projectile. DO NOT SET HIGHER THAN 4100.
                    MaxTrajectory = 5000f, //Maximum distance the projectile can travel before detonating/despawning.
                    FieldTime = 0, // 0 is disabled, a value causes the projectile to come to rest, spawn a field and remain for a time (Measured in game ticks, 60 = 1 second)
					GravityMultiplier = 0f, //1f=Gravity applies normal pull to projectile. 0 to disable.
                    SpeedVariance = Random(start: 0, end: 0), // subtracts value from DesiredSpeed
                    RangeVariance = Random(start: 0, end: 0), // subtracts value from MaxTrajectory
                    Smarts = new SmartsDef
                    {
                        Inaccuracy = 0, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
                        Aggressiveness = 3f, // controls how responsive tracking is.
                        MaxLateralThrust = .5f, // controls how sharp the trajectile may turn
                        TrackingDelay = 1, // Measured in Shape diameter units traveled.
                        MaxChaseTime = 2000, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                        OverideTarget = false, // when set to true ammo picks its own target, does not use hardpoint's.
                    },
                    Mines = new MinesDef
                    {
                        DetectRadius =  200,
                        DeCloakRadius = 100,
                        FieldTime = 1800,
                        Cloak = true,
                        Persist = false,
                    },
                },
                AmmoGraphics = new GraphicDef
                {
                    ModelName = "\\Models\\Akiad\\Small\\KrakenMissile.mwm", //USE DOUBLE LINES, ex: "\\Models\\Name\\Small\\MyGun.mwm"
                    VisualProbability = 1f, //Percentage chance for the model to spawn, "1f" for 100% chance.
                    ShieldHitDraw = true,
                    Particles = new AmmoParticleDef
                    {
                        Ammo = new ParticleDef
                        {
                            Name = "", //Particle that surrounds your projectile. ShipWelderArc
                            Color = Color(red: .6f, green: .6f, blue: 1f, alpha: 1),
                            Offset = Vector(x: 0, y: 0, z: 1),
                            Extras = new ParticleOptionDef
                            {
                                Loop = true,
                                Restart = false,
                                MaxDistance = 5000,
                                MaxDuration = 1,
                                Scale = 10f,
                            },
                        },
                        Hit = new ParticleDef
                        {
                            Name = "",
                            ApplyToShield = true,
                            ShrinkByDistance = true,
                            Color = Color(red: 3, green: 2, blue: 1, alpha: 1),
                            Offset = Vector(x: 0, y: 0, z: 0),
                            Extras = new ParticleOptionDef
                            {
                                Loop = false,
                                Restart = false,
                                MaxDistance = 500,
                                MaxDuration = 1,
                                Scale = 1,
                                HitPlayChance = 1f,
                            },
                        },
                    },
                    Lines = new LineDef
                    {
                        TracerMaterial = "ProjectileTrailLine", // Particle that follows your projectile. WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                        ColorVariance = Random(start: 5f, end: 10f), // multiply the color by random values within range.
                        WidthVariance = Random(start: 0f, end: 0.045f), // adds random value to default width (negatives shrinks width)
                        Tracer = new TracerBaseDef
                        {
                            Enable = true,
                            Length = .001f,
                            Width = .001f,
                            Color = Color(red: 0.9f, green: 0.9f, blue: 20f, alpha: 1),
                        },
                        Trail = new TrailDef
                        {
                            Enable = true,
                            Material = "WeaponLaser",
                            DecayTime = 15,
                            Color = Color(red: 20, green: 20, blue: 100, alpha: 1),
                            Back = true,
                            CustomWidth = .1f,
                            UseWidthVariance = false,
                            UseColorFade = true,
                        },
                        OffsetEffect = new OffsetEffectDef
                        {
                            MaxOffset =  0,// 0 offset value disables this effect
                            MinLength = 0.2f,
                            MaxLength = 3,
                        },
                    },
                },
                AmmoAudio = new AmmoAudioDef
                {
                    TravelSound = "",
                    HitSound = "ArcImpMetalMetalCat0",
                    HitPlayChance = 0.1f,
                    HitPlayShield = true,
                }, // Don't edit below this line

        };
		
		private AmmoDef Nova88mmHEAT => new AmmoDef
        {
            AmmoMagazine = "Nova88mmHEAT",
            AmmoRound = "88mm HEAT",
            HybridRound = false, //AmmoMagazine based weapon with energy cost
            EnergyCost = 0.00001f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
            BaseDamage = 8000f,
            Mass = 7.3f, // in kilograms
            Health = 0, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying.
            BackKickForce = 10000f,
            DecayPerShot = 0f,
            HardPointUsable = true, // set to false if this is a Fragment ammoType and you don't want the turret to be able to select it directly.
            EnergyMagazineSize = 12, //ONLY USE IF "AmmoMagazine=ENERGY". Otherwise specify magazine size in Ammo.sbc file.
            IgnoreWater = false,

            Shape = new ShapeDef //defines the collision shape of projectile, defaults line and visual Line Length if set to 0
            {
                Shape = LineShape, // LineShape or SphereShape. Do not use SphereShape for fast moving projectiles if you care about precision.
                Diameter = 1, // Diameter is minimum length of LineShape or minimum diameter of SphereShape
            },
            ObjectsHit = new ObjectsHitDef
            {
                MaxObjectsHit = 0, // 0 = disabled
                CountBlocks = false, // counts gridBlocks and not just entities hit
            },
            Fragment = new FragmentDef
            {
                AmmoRound = "HEShrapnel",
                Fragments = 20,
                Degrees = 5,
                Reverse = false,
                RandomizeDir = false, // randomize between forward and backward directions
            },
            Pattern = new PatternDef
            {
                Patterns = new[] {
                    "",
                },
                Enable = false,
                TriggerChance = 1f,
                Random = false,
                RandomMin = 1,
                RandomMax = 1,
                SkipParent = false,
                PatternSteps = 1, // Number of Patterns activated per round, will progress in order and loop.  Ignored if Random = true.
            },
            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0f, // 0 = disabled, 1000 = any blocks with currently integrity above 1000 will be immune to damage.
                DamageVoxels = false, // true = voxels are vulnerable to this weapon
                SelfDamage = false, // true = allow self damage.
                HealthHitModifier = 7, // defaults to a value of 1, this setting modifies how much Health is subtracted from a projectile per hit (1 = per hit).
                VoxelHitModifier = 10,
                Characters = 1f,
                // modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01 = 1% damage, 2 = 200% damage.
                FallOff = new FallOffDef
                {
                    Distance = 800f, // Distance at which max damage begins falling off.
                    MinMultipler = 0f, // value from 0.0f to 1f where 0.1f would be a min damage of 10% of max damage.
                },
                Grids = new GridSizeDef
                    {
                        Large = 1f,
                        Small = 1f,
                    },
                Armor = new ArmorDef
                {
                    Armor = 1f,
                    Light = -1f,
                    Heavy = 1f,
                    NonArmor = -1f,
                },
                Shields = new ShieldDef
                {
                    Modifier = 1f,
                    Type = Default,
                    BypassModifier = -1f,
                },
                // first true/false (ignoreOthers) will cause projectiles to pass through all blocks that do not match the custom subtypeIds.
                Custom = new CustomScalesDef
                {
                    IgnoreAllOthers = false,
                    Types = new[]
                    {
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test1",
                            Modifier = -1f,
                        },
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test2",
                            Modifier = -1f,
                        },
                    },
                },
            },
            AreaEffect = new AreaDamageDef
            {
                AreaEffect = Disabled, // Disabled = do not use area effect at all, Explosive, Radiant, AntiSmart, JumpNullField, JumpNullField, EnergySinkField, AnchorField, EmpField, OffenseField, NavField, DotField.
                Base = new AreaInfluence
                {
                    Radius = 5f, // the sphere of influence of area effects
                    EffectStrength = 3f, // For ewar it applies this amount per pulse/hit, non-ewar applies this as damage per tick per entity in area of influence. For radiant 0 == use spillover from BaseDamage, otherwise use this value.
                },
                Pulse = new PulseDef // interval measured in game ticks (60 == 1 second), pulseChance chance (0 - 100) that an entity in field will be hit
                {
                    Interval = 0,
                    PulseChance = 0,
                    GrowTime = 0,
                    HideModel = false,
                    ShowParticle = false,
                    Particle = new ParticleDef
                    {
                        Name = "", //ShipWelderArc
                        ShrinkByDistance = false,
                        Color = Color(red: 128, green: 0, blue: 0, alpha: 32),
                        Offset = Vector(x: 0, y: -1, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 1,
                            Scale = 1,
                        },
                    },
                },
                Explosions = new ExplosionDef
                {
                    NoVisuals = false,
                    NoSound = false,
                    NoShrapnel = false,
                    NoDeformation = false,
                    Scale = .5f,
                    CustomParticle = "",
                    CustomSound = "",
                },
                Detonation = new DetonateDef
                {
                    DetonateOnEnd = false,
                    ArmOnlyOnHit = false,
                    DetonationDamage = 0,
                    DetonationRadius = 0,
                    MinArmingTime = 0, //Min time in ticks before projectile will arm for detonation (will also affect Fragment spawning)
                },
                EwarFields = new EwarFieldsDef
                {
                    Duration = 1,
                    StackDuration = false,
                    Depletable = false,
                    MaxStacks = 0,
                    TriggerRange = 0f,
                    DisableParticleEffect = true,
                    Force = new PushPullDef // AreaEffectDamage is multiplied by target mass.
                    {
                        ForceFrom = ProjectileLastPosition, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
                        ForceTo = HitPosition, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
                        Position = TargetCenterOfMass, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
                    },
                },
            },
            Beams = new BeamDef
            {
                Enable = false,
                VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
                ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
                RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
                OneParticle = false, // Only spawn one particle hit per beam weapon.
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = None,
                TargetLossDegree = 80f,
                TargetLossTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                MaxLifeTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                AccelPerSec = 0f,
                DesiredSpeed = 400, // DO NOT SET HIGHER THAN 4100
                MaxTrajectory = 5100f,
                FieldTime = 0, // 0 is disabled, a value causes the projectile to come to rest, spawn a field and remain for a time (Measured in game ticks, 60 = 1 second)
                GravityMultiplier = 0f, // Gravity multiplier, influences the trajectory of the projectile, value greater than 0 to enable.
                SpeedVariance = Random(start: 0, end: 0), // subtracts value from DesiredSpeed
                RangeVariance = Random(start: 0, end: 0), // subtracts value from MaxTrajectory
                MaxTrajectoryTime = 0, // How long the weapon must fire before it reaches MaxTrajectory.
                Smarts = new SmartsDef
                {
                    Inaccuracy = 0f, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
                    Aggressiveness = 1f, // controls how responsive tracking is.
                    MaxLateralThrust = 0.5, // controls how sharp the trajectile may turn
                    TrackingDelay = 0, // Measured in Shape diameter units traveled.
                    MaxChaseTime = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    OverideTarget = true, // when set to true ammo picks its own target, does not use hardpoint's.
                    MaxTargets = 0, // Number of targets allowed before ending, 0 = unlimited
                    NoTargetExpire = false, // Expire without ever having a target at TargetLossTime
                    Roam = false, // Roam current area after target loss
                    KeepAliveAfterTargetLoss = false, // Whether to stop early death of projectile on target loss
                },
                Mines = new MinesDef
                {
                    DetectRadius = 0,
                    DeCloakRadius = 0,
                    FieldTime = 0,
                    Cloak = false,
                    Persist = false,
                },
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "\\Models\\Akiad\\Small\\88mmHEAT.mwm",
                VisualProbability = 1f,
                ShieldHitDraw = false,
                Particles = new AmmoParticleDef
                {
                    Ammo = new ParticleDef
                    {
                        Name = "", //ShipWelderArc
                        ShrinkByDistance = false,
                        Color = Color(red: 128, green: 0, blue: 0, alpha: 32),
                        Offset = Vector(x: 0, y: -1, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 1,
                            Scale = 1,
                        },
                    },
                    Hit = new ParticleDef
                    {
                        Name = "",
                        ApplyToShield = true,
                        ShrinkByDistance = false,
                        Color = Color(red: 3, green: 1.9f, blue: 1f, alpha: 1),
                        Offset = Vector(x: 0, y: 0, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 30,
                            Scale = 1,
                            HitPlayChance = 1f,
                        },
                    },
                    Eject = new ParticleDef
                    {
                        Name = "",
                        ApplyToShield = true,
                        ShrinkByDistance = false,
                        Color = Color(red: 128, green: 1.9f, blue: 1f, alpha: 1),
                        Offset = Vector(x: 0, y: 0, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 30,
                            Scale = 1,
                            HitPlayChance = 1f,
                        },
                    },
                },
                Lines = new LineDef
                {
                    ColorVariance = Random(start: 0.75f, end: 2f), // multiply the color by random values within range.
                    WidthVariance = Random(start: 0f, end: 0f), // adds random value to default width (negatives shrinks width)
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = 1f,
                        Width = 0.1f,
                        Color = Color(red: 3, green: 2, blue: 1f, alpha: 1),
                        VisualFadeStart = 0, // Number of ticks the weapon has been firing before projectiles begin to fade their color
                        VisualFadeEnd = 0, // How many ticks after fade began before it will be invisible.
                        Textures = new[] {// WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                            "WeaponLaser",
                        },
                        TextureMode = Normal, // Normal, Cycle, Chaos, Wave
                        Segmentation = new SegmentDef
                        {
                            Enable = false, // If true Tracer TextureMode is ignored
                            Textures = new[] {
								"",
                            },
                            SegmentLength = 0f, // Uses the values below.
                            SegmentGap = 0f, // Uses Tracer textures and values
                            Speed = 1f, // meters per second
                            Color = Color(red: 1, green: 2, blue: 2.5f, alpha: 1),
                            WidthMultiplier = 1f,
                            Reverse = false,
                            UseLineVariance = true,
                            WidthVariance = Random(start: 0f, end: 0f),
                            ColorVariance = Random(start: 0f, end: 0f)
                        }
                    },
                    Trail = new TrailDef
                    {
                        Enable = false,
                        Textures = new[] {
							"",
                        },
                        TextureMode = Normal,
                        DecayTime = 128,
                        Color = Color(red: 0, green: 0, blue: 1, alpha: 1),
                        Back = false,
                        CustomWidth = 0,
                        UseWidthVariance = false,
                        UseColorFade = true,
                    },
                    OffsetEffect = new OffsetEffectDef
                    {
                        MaxOffset = 0,// 0 offset value disables this effect
                        MinLength = 0.2f,
                        MaxLength = 3,
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                TravelSound = "",
                HitSound = "",
                ShieldHitSound = "",
                PlayerHitSound = "",
                VoxelHitSound = "",
                FloatingHitSound = "",
                HitPlayChance = 0.5f,
                HitPlayShield = true,
            }, // Don't edit below this line
            Ejection = new EjectionDef
            {
                Type = Particle, // Particle or Item (Inventory Component)
                Speed = 100f, // Speed inventory is ejected from in dummy direction
                SpawnChance = 0.5f, // chance of triggering effect (0 - 1)
                CompDef = new ComponentDef
                {
                    ItemName = "", //InventoryComponent name
                    ItemLifeTime = 0, // how long item should exist in world
                    Delay = 0, // delay in ticks after shot before ejected
                }
            },
        };
		private AmmoDef Eris88mmStack => new AmmoDef
        {
            AmmoMagazine = "Energy",
            AmmoRound = "DesignatorLaser",
            HybridRound = false, //AmmoMagazine based weapon with energy cost
            EnergyCost = 0.01f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
            BaseDamage = 2000f,
            Mass = 7.3f, // in kilograms
            Health = 0, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying.
            BackKickForce = 1000f,
            DecayPerShot = 0f,
            HardPointUsable = true, // set to false if this is a Fragment ammoType and you don't want the turret to be able to select it directly.
            EnergyMagazineSize = 72, //ONLY USE IF "AmmoMagazine=ENERGY". Otherwise specify magazine size in Ammo.sbc file.
            IgnoreWater = false,

            Shape = new ShapeDef //defines the collision shape of projectile, defaults line and visual Line Length if set to 0
            {
                Shape = LineShape, // LineShape or SphereShape. Do not use SphereShape for fast moving projectiles if you care about precision.
                Diameter = 0.1f, // Diameter is minimum length of LineShape or minimum diameter of SphereShape
            },
            ObjectsHit = new ObjectsHitDef
            {
                MaxObjectsHit = 0, // 0 = disabled
                CountBlocks = false, // counts gridBlocks and not just entities hit
            },
            Fragment = new FragmentDef
            {
                AmmoRound = "OrionOrbShrapnel",
                Fragments = 20,
                Degrees = 15,
                Reverse = false,
                RandomizeDir = false, // randomize between forward and backward directions
            },
            Pattern = new PatternDef
            {
                Patterns = new[] {
                    "",
                },
                Enable = false,
                TriggerChance = 1f,
                Random = false,
                RandomMin = 1,
                RandomMax = 1,
                SkipParent = false,
                PatternSteps = 1, // Number of Patterns activated per round, will progress in order and loop.  Ignored if Random = true.
            },
            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0f, // 0 = disabled, 1000 = any blocks with currently integrity above 1000 will be immune to damage.
                DamageVoxels = false, // true = voxels are vulnerable to this weapon
                SelfDamage = false, // true = allow self damage.
                HealthHitModifier = 1, // defaults to a value of 1, this setting modifies how much Health is subtracted from a projectile per hit (1 = per hit).
                VoxelHitModifier = 10,
                Characters = 1f,
                // modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01 = 1% damage, 2 = 200% damage.
                FallOff = new FallOffDef
                {
                    Distance = 1800f, // Distance at which max damage begins falling off.
                    MinMultipler = 0f, // value from 0.0f to 1f where 0.1f would be a min damage of 10% of max damage.
                },
                Grids = new GridSizeDef
                    {
                        Large = 1f,
                        Small = -1f,
                    },
                Armor = new ArmorDef
                {
                    Armor = 1f,
                    Light = 1f,
                    Heavy = 1f,
                    NonArmor = -1f,
                },
                Shields = new ShieldDef
                {
                    Modifier = 1f,
                    Type = Default,
                    BypassModifier = -1f,
                },
                // first true/false (ignoreOthers) will cause projectiles to pass through all blocks that do not match the custom subtypeIds.
                Custom = new CustomScalesDef
                {
                    IgnoreAllOthers = false,
                    Types = new[]
                    {
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test1",
                            Modifier = -1f,
                        },
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test2",
                            Modifier = -1f,
                        },
                    },
                },
            },
            AreaEffect = new AreaDamageDef
            {
                AreaEffect = Explosive, // Disabled = do not use area effect at all, Explosive, Radiant, AntiSmart, JumpNullField, JumpNullField, EnergySinkField, AnchorField, EmpField, OffenseField, NavField, DotField.
                Base = new AreaInfluence
                {
                    Radius = 5f, // the sphere of influence of area effects
                    EffectStrength = 3f, // For ewar it applies this amount per pulse/hit, non-ewar applies this as damage per tick per entity in area of influence. For radiant 0 == use spillover from BaseDamage, otherwise use this value.
                },
                Pulse = new PulseDef // interval measured in game ticks (60 == 1 second), pulseChance chance (0 - 100) that an entity in field will be hit
                {
                    Interval = 0,
                    PulseChance = 0,
                    GrowTime = 0,
                    HideModel = false,
                    ShowParticle = false,
                    Particle = new ParticleDef
                    {
                        Name = "", //ShipWelderArc
                        ShrinkByDistance = false,
                        Color = Color(red: 128, green: 0, blue: 0, alpha: 32),
                        Offset = Vector(x: 0, y: -1, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 1,
                            Scale = 1,
                        },
                    },
                },
                Explosions = new ExplosionDef
                {
                    NoVisuals = false,
                    NoSound = false,
                    NoShrapnel = false,
                    NoDeformation = false,
                    Scale = 5f,
                    CustomParticle = "ArbalestBlast",
                    CustomSound = "",
                },
                Detonation = new DetonateDef
                {
                    DetonateOnEnd = false,
                    ArmOnlyOnHit = false,
                    DetonationDamage = 0,
                    DetonationRadius = 0,
                    MinArmingTime = 0, //Min time in ticks before projectile will arm for detonation (will also affect Fragment spawning)
                },
                EwarFields = new EwarFieldsDef
                {
                    Duration = 1,
                    StackDuration = false,
                    Depletable = false,
                    MaxStacks = 0,
                    TriggerRange = 0f,
                    DisableParticleEffect = true,
                    Force = new PushPullDef // AreaEffectDamage is multiplied by target mass.
                    {
                        ForceFrom = ProjectileLastPosition, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
                        ForceTo = HitPosition, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
                        Position = TargetCenterOfMass, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
                    },
                },
            },
            Beams = new BeamDef
            {
                Enable = false,
                VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
                ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
                RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
                OneParticle = false, // Only spawn one particle hit per beam weapon.
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = None,
                TargetLossDegree = 80f,
                TargetLossTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                MaxLifeTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                AccelPerSec = 0f,
                DesiredSpeed = 800, // DO NOT SET HIGHER THAN 4100
                MaxTrajectory = 5000f,
                FieldTime = 0, // 0 is disabled, a value causes the projectile to come to rest, spawn a field and remain for a time (Measured in game ticks, 60 = 1 second)
                GravityMultiplier = 0f, // Gravity multiplier, influences the trajectory of the projectile, value greater than 0 to enable.
                SpeedVariance = Random(start: 0, end: 0), // subtracts value from DesiredSpeed
                RangeVariance = Random(start: 0, end: 0), // subtracts value from MaxTrajectory
                MaxTrajectoryTime = 0, // How long the weapon must fire before it reaches MaxTrajectory.
                Smarts = new SmartsDef
                {
                    Inaccuracy = 0f, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
                    Aggressiveness = 1f, // controls how responsive tracking is.
                    MaxLateralThrust = 0.5, // controls how sharp the trajectile may turn
                    TrackingDelay = 0, // Measured in Shape diameter units traveled.
                    MaxChaseTime = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    OverideTarget = true, // when set to true ammo picks its own target, does not use hardpoint's.
                    MaxTargets = 0, // Number of targets allowed before ending, 0 = unlimited
                    NoTargetExpire = false, // Expire without ever having a target at TargetLossTime
                    Roam = false, // Roam current area after target loss
                    KeepAliveAfterTargetLoss = false, // Whether to stop early death of projectile on target loss
                },
                Mines = new MinesDef
                {
                    DetectRadius = 0,
                    DeCloakRadius = 0,
                    FieldTime = 0,
                    Cloak = false,
                    Persist = false,
                },
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "",
                VisualProbability = 1f,
                ShieldHitDraw = false,
                Particles = new AmmoParticleDef
                {
                    Ammo = new ParticleDef
                    {
                        Name = "ArbalestBlast", //ShipWelderArc
                        ShrinkByDistance = false,
                        Color = Color(red: 10, green: 1, blue: 1, alpha: 32),
                        Offset = Vector(x: 0, y: -1, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 1,
                            Scale = 1,
                        },
                    },
                    Hit = new ParticleDef
                    {
                        Name = "ArbalestBlast",
                        ApplyToShield = true,
                        ShrinkByDistance = false,
                        Color = Color(red: 3, green: 1f, blue: 1f, alpha: 1),
                        Offset = Vector(x: 0, y: 0, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 30,
                            Scale = 1,
                            HitPlayChance = 1f,
                        },
                    },
                    Eject = new ParticleDef
                    {
                        Name = "",
                        ApplyToShield = true,
                        ShrinkByDistance = false,
                        Color = Color(red: 128, green: 1.9f, blue: 1f, alpha: 1),
                        Offset = Vector(x: 0, y: 0, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 30,
                            Scale = 1,
                            HitPlayChance = 1f,
                        },
                    },
                },
                Lines = new LineDef
                {
                    ColorVariance = Random(start: 0.75f, end: 2f), // multiply the color by random values within range.
                    WidthVariance = Random(start: 0f, end: 0f), // adds random value to default width (negatives shrinks width)
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = .0001f,
                        Width = 0.0001f,
                        Color = Color(red: 3, green: 2, blue: 1f, alpha: 1),
                        VisualFadeStart = 0, // Number of ticks the weapon has been firing before projectiles begin to fade their color
                        VisualFadeEnd = 0, // How many ticks after fade began before it will be invisible.
                        Textures = new[] {// WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                            "WeaponLaser",
                        },
                        TextureMode = Normal, // Normal, Cycle, Chaos, Wave
                        Segmentation = new SegmentDef
                        {
                            Enable = false, // If true Tracer TextureMode is ignored
                            Textures = new[] {
								"",
                            },
                            SegmentLength = 0f, // Uses the values below.
                            SegmentGap = 0f, // Uses Tracer textures and values
                            Speed = 1f, // meters per second
                            Color = Color(red: 1, green: 2, blue: 2.5f, alpha: 1),
                            WidthMultiplier = 1f,
                            Reverse = false,
                            UseLineVariance = true,
                            WidthVariance = Random(start: 0f, end: 0f),
                            ColorVariance = Random(start: 0f, end: 0f)
                        }
                    },
                    Trail = new TrailDef
                    {
                        Enable = true,
                        Textures = new[] {
							"ProjectileTrailLine",
                        },
                        TextureMode = Normal,
                        DecayTime = 30,
                        Color = Color(red: 10, green: 1, blue: 1, alpha: 1),
                        Back = false,
                        CustomWidth = .5f,
                        UseWidthVariance = false,
                        UseColorFade = true,
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                TravelSound = "",
                HitSound = "",
                ShieldHitSound = "",
                PlayerHitSound = "",
                VoxelHitSound = "",
                FloatingHitSound = "",
                HitPlayChance = 0.5f,
                HitPlayShield = true,
            }, // Don't edit below this line
            Ejection = new EjectionDef
            {
                Type = Particle, // Particle or Item (Inventory Component)
                Speed = 100f, // Speed inventory is ejected from in dummy direction
                SpawnChance = 0.5f, // chance of triggering effect (0 - 1)
                CompDef = new ComponentDef
                {
                    ItemName = "", //InventoryComponent name
                    ItemLifeTime = 0, // how long item should exist in world
                    Delay = 0, // delay in ticks after shot before ejected
                }
            },
        };
		
		private AmmoDef Lancer50AP => new AmmoDef
        {
            AmmoMagazine = "Lancer50AP",
            AmmoRound = "50mm AP",
            HybridRound = false, //AmmoMagazine based weapon with energy cost
            EnergyCost = 0.00001f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
            BaseDamage = 6000f,
            Mass = 1f, // in kilograms
            Health = 0, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying.
            BackKickForce = 5000f,
            DecayPerShot = 0f,
            HardPointUsable = true, // set to false if this is a Fragment ammoType and you don't want the turret to be able to select it directly.
            EnergyMagazineSize = 12, //ONLY USE IF "AmmoMagazine=ENERGY". Otherwise specify magazine size in Ammo.sbc file.
            IgnoreWater = false,

            Shape = new ShapeDef //defines the collision shape of projectile, defaults line and visual Line Length if set to 0
            {
                Shape = LineShape, // LineShape or SphereShape. Do not use SphereShape for fast moving projectiles if you care about precision.
                Diameter = 1, // Diameter is minimum length of LineShape or minimum diameter of SphereShape
            },
            ObjectsHit = new ObjectsHitDef
            {
                MaxObjectsHit = 0, // 0 = disabled
                CountBlocks = false, // counts gridBlocks and not just entities hit
            },
            Fragment = new FragmentDef
            {
                AmmoRound = "",
                Fragments = 0,
                Degrees = 15,
                Reverse = false,
                RandomizeDir = false, // randomize between forward and backward directions
            },
            Pattern = new PatternDef
            {
                Patterns = new[] {
                    "",
                },
                Enable = false,
                TriggerChance = 1f,
                Random = false,
                RandomMin = 1,
                RandomMax = 1,
                SkipParent = false,
                PatternSteps = 1, // Number of Patterns activated per round, will progress in order and loop.  Ignored if Random = true.
            },
            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0f, // 0 = disabled, 1000 = any blocks with currently integrity above 1000 will be immune to damage.
                DamageVoxels = false, // true = voxels are vulnerable to this weapon
                SelfDamage = false, // true = allow self damage.
                HealthHitModifier = 20, // defaults to a value of 1, this setting modifies how much Health is subtracted from a projectile per hit (1 = per hit).
                VoxelHitModifier = 10,
                Characters = 1f,
                // modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01 = 1% damage, 2 = 200% damage.
                FallOff = new FallOffDef
                {
                    Distance = 2000f, // Distance at which max damage begins falling off.
                    MinMultipler = 0f, // value from 0.0f to 1f where 0.1f would be a min damage of 10% of max damage.
                },
                Grids = new GridSizeDef
                    {
                        Large = 1f,
                        Small = 1f,
                    },
                Armor = new ArmorDef
                {
                    Armor = 1f,
                    Light = -1f,
                    Heavy = 1f,
                    NonArmor = -1f,
                },
                Shields = new ShieldDef
                {
                    Modifier = 1.5f,
                    Type = Default,
                    BypassModifier = -1f,
                },
                // first true/false (ignoreOthers) will cause projectiles to pass through all blocks that do not match the custom subtypeIds.
                Custom = new CustomScalesDef
                {
                    IgnoreAllOthers = false,
                    Types = new[]
                    {
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test1",
                            Modifier = -1f,
                        },
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test2",
                            Modifier = -1f,
                        },
                    },
                },
            },
            AreaEffect = new AreaDamageDef
            {
                AreaEffect = Disabled, // Disabled = do not use area effect at all, Explosive, Radiant, AntiSmart, JumpNullField, JumpNullField, EnergySinkField, AnchorField, EmpField, OffenseField, NavField, DotField.
                Base = new AreaInfluence
                {
                    Radius = 10f, // the sphere of influence of area effects
                    EffectStrength = 0f, // For ewar it applies this amount per pulse/hit, non-ewar applies this as damage per tick per entity in area of influence. For radiant 0 == use spillover from BaseDamage, otherwise use this value.
                },
                Pulse = new PulseDef // interval measured in game ticks (60 == 1 second), pulseChance chance (0 - 100) that an entity in field will be hit
                {
                    Interval = 0,
                    PulseChance = 0,
                    GrowTime = 0,
                    HideModel = false,
                    ShowParticle = false,
                    Particle = new ParticleDef
                    {
                        Name = "", //ShipWelderArc
                        ShrinkByDistance = false,
                        Color = Color(red: 128, green: 0, blue: 0, alpha: 32),
                        Offset = Vector(x: 0, y: -1, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 1,
                            Scale = 1,
                        },
                    },
                },
                Explosions = new ExplosionDef
                {
                    NoVisuals = false,
                    NoSound = false,
                    NoShrapnel = false,
                    NoDeformation = true,
                    Scale = 1,
                    CustomParticle = "",
                    CustomSound = "",
                },
                Detonation = new DetonateDef
                {
                    DetonateOnEnd = false,
                    ArmOnlyOnHit = false,
                    DetonationDamage = 0,
                    DetonationRadius = 0,
                    MinArmingTime = 0, //Min time in ticks before projectile will arm for detonation (will also affect Fragment spawning)
                },
                EwarFields = new EwarFieldsDef
                {
                    Duration = 1,
                    StackDuration = false,
                    Depletable = false,
                    MaxStacks = 0,
                    TriggerRange = 0f,
                    DisableParticleEffect = true,
                    Force = new PushPullDef // AreaEffectDamage is multiplied by target mass.
                    {
                        ForceFrom = ProjectileLastPosition, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
                        ForceTo = HitPosition, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
                        Position = TargetCenterOfMass, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
                    },
                },
            },
            Beams = new BeamDef
            {
                Enable = false,
                VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
                ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
                RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
                OneParticle = false, // Only spawn one particle hit per beam weapon.
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = None,
                TargetLossDegree = 80f,
                TargetLossTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                MaxLifeTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                AccelPerSec = 0f,
                DesiredSpeed = 700, // DO NOT SET HIGHER THAN 4100
                MaxTrajectory = 5000f,
                FieldTime = 0, // 0 is disabled, a value causes the projectile to come to rest, spawn a field and remain for a time (Measured in game ticks, 60 = 1 second)
                GravityMultiplier = 0f, // Gravity multiplier, influences the trajectory of the projectile, value greater than 0 to enable.
                SpeedVariance = Random(start: 0, end: 0), // subtracts value from DesiredSpeed
                RangeVariance = Random(start: 0, end: 0), // subtracts value from MaxTrajectory
                MaxTrajectoryTime = 0, // How long the weapon must fire before it reaches MaxTrajectory.
                Smarts = new SmartsDef
                {
                    Inaccuracy = 1f, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
                    Aggressiveness = 1f, // controls how responsive tracking is.
                    MaxLateralThrust = 0.5, // controls how sharp the trajectile may turn
                    TrackingDelay = 0, // Measured in Shape diameter units traveled.
                    MaxChaseTime = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    OverideTarget = true, // when set to true ammo picks its own target, does not use hardpoint's.
                    MaxTargets = 0, // Number of targets allowed before ending, 0 = unlimited
                    NoTargetExpire = false, // Expire without ever having a target at TargetLossTime
                    Roam = false, // Roam current area after target loss
                    KeepAliveAfterTargetLoss = false, // Whether to stop early death of projectile on target loss
                },
                Mines = new MinesDef
                {
                    DetectRadius = 0,
                    DeCloakRadius = 0,
                    FieldTime = 0,
                    Cloak = false,
                    Persist = false,
                },
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "",
                VisualProbability = 1f,
                ShieldHitDraw = false,
                Particles = new AmmoParticleDef
                {
                    Ammo = new ParticleDef
                    {
                        Name = "", //ShipWelderArc
                        ShrinkByDistance = false,
                        Color = Color(red: 128, green: 0, blue: 0, alpha: 32),
                        Offset = Vector(x: 0, y: -1, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 1,
                            Scale = 1,
                        },
                    },
                    Hit = new ParticleDef
                    {
                        Name = "",
                        ApplyToShield = true,
                        ShrinkByDistance = false,
                        Color = Color(red: 3, green: 1.9f, blue: 1f, alpha: 1),
                        Offset = Vector(x: 0, y: 0, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 30,
                            Scale = 1,
                            HitPlayChance = 1f,
                        },
                    },
                    Eject = new ParticleDef
                    {
                        Name = "",
                        ApplyToShield = true,
                        ShrinkByDistance = false,
                        Color = Color(red: 3, green: 1.9f, blue: 1f, alpha: 1),
                        Offset = Vector(x: 0, y: 0, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 30,
                            Scale = 1,
                            HitPlayChance = 1f,
                        },
                    },
                },
                Lines = new LineDef
                {
                    ColorVariance = Random(start: 0.75f, end: 2f), // multiply the color by random values within range.
                    WidthVariance = Random(start: 0f, end: 0f), // adds random value to default width (negatives shrinks width)
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = 1f,
                        Width = 0.2f,
                        Color = Color(red: 10, green: 2, blue: 1f, alpha: 1),
                        VisualFadeStart = 0, // Number of ticks the weapon has been firing before projectiles begin to fade their color
                        VisualFadeEnd = 0, // How many ticks after fade began before it will be invisible.
                        Textures = new[] {// WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                            "WeaponLaser",
                        },
                        TextureMode = Normal, // Normal, Cycle, Chaos, Wave
                        Segmentation = new SegmentDef
                        {
                            Enable = false, // If true Tracer TextureMode is ignored
                            Textures = new[] {
								"",
                            },
                            SegmentLength = 0f, // Uses the values below.
                            SegmentGap = 0f, // Uses Tracer textures and values
                            Speed = 1f, // meters per second
                            Color = Color(red: 1, green: 2, blue: 2.5f, alpha: 1),
                            WidthMultiplier = 1f,
                            Reverse = false,
                            UseLineVariance = true,
                            WidthVariance = Random(start: 0f, end: 0f),
                            ColorVariance = Random(start: 0f, end: 0f)
                        }
                    },
                    Trail = new TrailDef
                    {
                        Enable = false,
                        Textures = new[] {
							"",
                        },
                        TextureMode = Normal,
                        DecayTime = 128,
                        Color = Color(red: 0, green: 0, blue: 1, alpha: 1),
                        Back = false,
                        CustomWidth = 0,
                        UseWidthVariance = false,
                        UseColorFade = true,
                    },
                    OffsetEffect = new OffsetEffectDef
                    {
                        MaxOffset = 0,// 0 offset value disables this effect
                        MinLength = 0.2f,
                        MaxLength = 3,
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                TravelSound = "",
                HitSound = "",
                ShieldHitSound = "",
                PlayerHitSound = "",
                VoxelHitSound = "",
                FloatingHitSound = "",
                HitPlayChance = 0.5f,
                HitPlayShield = true,
            }, // Don't edit below this line
            Ejection = new EjectionDef
            {
                Type = Particle, // Particle or Item (Inventory Component)
                Speed = 100f, // Speed inventory is ejected from in dummy direction
                SpawnChance = 0.5f, // chance of triggering effect (0 - 1)
                CompDef = new ComponentDef
                {
                    ItemName = "", //InventoryComponent name
                    ItemLifeTime = 0, // how long item should exist in world
                    Delay = 0, // delay in ticks after shot before ejected
                }
            },
        };
		
		private AmmoDef OrionOrb => new AmmoDef
        {
            AmmoMagazine = "OrionOrb",
            AmmoRound = "Orion Orb",
            HybridRound = false, //AmmoMagazine based weapon with energy cost
            EnergyCost = 0.00000000001f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
            BaseDamage = 1000f,
            Mass = .3f, // in kilograms
            Health = 0, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying.
            BackKickForce = 1000f,
            DecayPerShot = 0f,
            HardPointUsable = true, // set to false if this is a Fragment ammoType and you don't want the turret to be able to select it directly.
            EnergyMagazineSize = 12, //ONLY USE IF "AmmoMagazine=ENERGY". Otherwise specify magazine size in Ammo.sbc file.
            IgnoreWater = false,

            Shape = new ShapeDef //defines the collision shape of projectile, defaults line and visual Line Length if set to 0
            {
                Shape = LineShape, // LineShape or SphereShape. Do not use SphereShape for fast moving projectiles if you care about precision.
                Diameter = 1, // Diameter is minimum length of LineShape or minimum diameter of SphereShape
            },
            ObjectsHit = new ObjectsHitDef
            {
                MaxObjectsHit = 0, // 0 = disabled
                CountBlocks = false, // counts gridBlocks and not just entities hit
            },
            Fragment = new FragmentDef
            {
                AmmoRound = "OrionOrbShrapnel",
                Fragments = 1,
                Degrees = 15,
                Reverse = false,
                RandomizeDir = false, // randomize between forward and backward directions
            },
            Pattern = new PatternDef
            {
                Patterns = new[] {
                    "",
                },
                Enable = false,
                TriggerChance = 1f,
                Random = false,
                RandomMin = 1,
                RandomMax = 1,
                SkipParent = false,
                PatternSteps = 1, // Number of Patterns activated per round, will progress in order and loop.  Ignored if Random = true.
            },
            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0f, // 0 = disabled, 1000 = any blocks with currently integrity above 1000 will be immune to damage.
                DamageVoxels = false, // true = voxels are vulnerable to this weapon
                SelfDamage = false, // true = allow self damage.
                HealthHitModifier = 2, // defaults to a value of 1, this setting modifies how much Health is subtracted from a projectile per hit (1 = per hit).
                VoxelHitModifier = 10,
                Characters = 1f,
                // modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01 = 1% damage, 2 = 200% damage.
                FallOff = new FallOffDef
                {
                    Distance = 1000f, // Distance at which max damage begins falling off.
                    MinMultipler = 0f, // value from 0.0f to 1f where 0.1f would be a min damage of 10% of max damage.
                },
                Grids = new GridSizeDef
                    {
                        Large = 1f,
                        Small = 1f,
                    },
                Armor = new ArmorDef
                {
                    Armor = 1f,
                    Light = -1f,
                    Heavy = -1f,
                    NonArmor = 1f,
                },
                Shields = new ShieldDef
                {
                    Modifier = 1f,
                    Type = Default,
                    BypassModifier = -1f,
                },
                // first true/false (ignoreOthers) will cause projectiles to pass through all blocks that do not match the custom subtypeIds.
                Custom = new CustomScalesDef
                {
                    IgnoreAllOthers = false,
                    Types = new[]
                    {
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test1",
                            Modifier = -1f,
                        },
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test2",
                            Modifier = -1f,
                        },
                    },
                },
            },
            AreaEffect = new AreaDamageDef
            {
                AreaEffect = Disabled, // Disabled = do not use area effect at all, Explosive, Radiant, AntiSmart, JumpNullField, JumpNullField, EnergySinkField, AnchorField, EmpField, OffenseField, NavField, DotField.
                Base = new AreaInfluence
                {
                    Radius = 10f, // the sphere of influence of area effects
                    EffectStrength = 0f, // For ewar it applies this amount per pulse/hit, non-ewar applies this as damage per tick per entity in area of influence. For radiant 0 == use spillover from BaseDamage, otherwise use this value.
                },
                Pulse = new PulseDef // interval measured in game ticks (60 == 1 second), pulseChance chance (0 - 100) that an entity in field will be hit
                {
                    Interval = 0,
                    PulseChance = 0,
                    GrowTime = 0,
                    HideModel = false,
                    ShowParticle = false,
                    Particle = new ParticleDef
                    {
                        Name = "", //ShipWelderArc
                        ShrinkByDistance = false,
                        Color = Color(red: 128, green: 0, blue: 0, alpha: 32),
                        Offset = Vector(x: 0, y: -1, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 1,
                            Scale = 1,
                        },
                    },
                },
                Explosions = new ExplosionDef
                {
                    NoVisuals = false,
                    NoSound = false,
                    NoShrapnel = false,
                    NoDeformation = true,
                    Scale = 1,
                    CustomParticle = "",
                    CustomSound = "",
                },
                Detonation = new DetonateDef
                {
                    DetonateOnEnd = false,
                    ArmOnlyOnHit = false,
                    DetonationDamage = 0,
                    DetonationRadius = 0,
                    MinArmingTime = 0, //Min time in ticks before projectile will arm for detonation (will also affect Fragment spawning)
                },
                EwarFields = new EwarFieldsDef
                {
                    Duration = 1,
                    StackDuration = false,
                    Depletable = false,
                    MaxStacks = 0,
                    TriggerRange = 0f,
                    DisableParticleEffect = true,
                    Force = new PushPullDef // AreaEffectDamage is multiplied by target mass.
                    {
                        ForceFrom = ProjectileLastPosition, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
                        ForceTo = HitPosition, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
                        Position = TargetCenterOfMass, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
                    },
                },
            },
            Beams = new BeamDef
            {
                Enable = false,
                VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
                ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
                RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
                OneParticle = false, // Only spawn one particle hit per beam weapon.
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = None,
                TargetLossDegree = 80f,
                TargetLossTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                MaxLifeTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                AccelPerSec = 0f,
                DesiredSpeed = 50, // DO NOT SET HIGHER THAN 4100
                MaxTrajectory = 5000f,
                FieldTime = 0, // 0 is disabled, a value causes the projectile to come to rest, spawn a field and remain for a time (Measured in game ticks, 60 = 1 second)
                GravityMultiplier = 1f, // Gravity multiplier, influences the trajectory of the projectile, value greater than 0 to enable.
                SpeedVariance = Random(start: 0, end: 0), // subtracts value from DesiredSpeed
                RangeVariance = Random(start: 0, end: 0), // subtracts value from MaxTrajectory
                MaxTrajectoryTime = 0, // How long the weapon must fire before it reaches MaxTrajectory.
                Smarts = new SmartsDef
                {
                    Inaccuracy = 0f, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
                    Aggressiveness = 1f, // controls how responsive tracking is.
                    MaxLateralThrust = 0.5, // controls how sharp the trajectile may turn
                    TrackingDelay = 0, // Measured in Shape diameter units traveled.
                    MaxChaseTime = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    OverideTarget = true, // when set to true ammo picks its own target, does not use hardpoint's.
                    MaxTargets = 0, // Number of targets allowed before ending, 0 = unlimited
                    NoTargetExpire = false, // Expire without ever having a target at TargetLossTime
                    Roam = false, // Roam current area after target loss
                    KeepAliveAfterTargetLoss = false, // Whether to stop early death of projectile on target loss
                },
                Mines = new MinesDef
                {
                    DetectRadius = 0,
                    DeCloakRadius = 0,
                    FieldTime = 0,
                    Cloak = false,
                    Persist = false,
                },
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "\\Models\\Akiad\\Small\\OrionGlobe.mwm",
                VisualProbability = 1f,
                ShieldHitDraw = false,
                Particles = new AmmoParticleDef
                {
                    Ammo = new ParticleDef
                    {
                        Name = "", //ShipWelderArc
                        ShrinkByDistance = false,
                        Color = Color(red: 128, green: 0, blue: 0, alpha: 32),
                        Offset = Vector(x: 0, y: -1, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 1,
                            Scale = 1,
                        },
                    },
                    Hit = new ParticleDef
                    {
                        Name = "PlasmaBolt",
                        ApplyToShield = true,
                        ShrinkByDistance = false,
                        Color = Color(red: 3, green: 1.9f, blue: .5f, alpha: 1),
                        Offset = Vector(x: 0, y: 0, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 30,
                            Scale = 1,
                            HitPlayChance = 1f,
                        },
                    },
                    Eject = new ParticleDef
                    {
                        Name = "",
                        ApplyToShield = true,
                        ShrinkByDistance = false,
                        Color = Color(red: 3, green: 1.9f, blue: 1f, alpha: 1),
                        Offset = Vector(x: 0, y: 0, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 30,
                            Scale = 1,
                            HitPlayChance = 1f,
                        },
                    },
                },
                Lines = new LineDef
                {
                    ColorVariance = Random(start: 0.75f, end: 2f), // multiply the color by random values within range.
                    WidthVariance = Random(start: 0f, end: 0f), // adds random value to default width (negatives shrinks width)
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = 0.1f,
                        Width = 0.1f,
                        Color = Color(red: 3, green: 2, blue: 1f, alpha: 1),
                        VisualFadeStart = 0, // Number of ticks the weapon has been firing before projectiles begin to fade their color
                        VisualFadeEnd = 0, // How many ticks after fade began before it will be invisible.
                        Textures = new[] {// WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                            "WeaponLaser",
                        },
                        TextureMode = Normal, // Normal, Cycle, Chaos, Wave
                        Segmentation = new SegmentDef
                        {
                            Enable = false, // If true Tracer TextureMode is ignored
                            Textures = new[] {
								"",
                            },
                            SegmentLength = 0f, // Uses the values below.
                            SegmentGap = 0f, // Uses Tracer textures and values
                            Speed = 1f, // meters per second
                            Color = Color(red: 1, green: 2, blue: 2.5f, alpha: 1),
                            WidthMultiplier = 1f,
                            Reverse = false,
                            UseLineVariance = true,
                            WidthVariance = Random(start: 0f, end: 0f),
                            ColorVariance = Random(start: 0f, end: 0f)
                        }
                    },
                    Trail = new TrailDef
                    {
                        Enable = false,
                        Textures = new[] {
							"",
                        },
                        TextureMode = Normal,
                        DecayTime = 128,
                        Color = Color(red: 0, green: 0, blue: 1, alpha: 1),
                        Back = false,
                        CustomWidth = 0,
                        UseWidthVariance = false,
                        UseColorFade = true,
                    },
                    OffsetEffect = new OffsetEffectDef
                    {
                        MaxOffset = 0,// 0 offset value disables this effect
                        MinLength = 0.2f,
                        MaxLength = 3,
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                TravelSound = "",
                HitSound = "",
                ShieldHitSound = "",
                PlayerHitSound = "",
                VoxelHitSound = "",
                FloatingHitSound = "",
                HitPlayChance = 0.5f,
                HitPlayShield = true,
            }, // Don't edit below this line
            Ejection = new EjectionDef
            {
                Type = Particle, // Particle or Item (Inventory Component)
                Speed = 100f, // Speed inventory is ejected from in dummy direction
                SpawnChance = 0.5f, // chance of triggering effect (0 - 1)
                CompDef = new ComponentDef
                {
                    ItemName = "", //InventoryComponent name
                    ItemLifeTime = 0, // how long item should exist in world
                    Delay = 0, // delay in ticks after shot before ejected
                }
            },
        };
		
		private AmmoDef OrionOrbShrapnel => new AmmoDef
        {
            AmmoMagazine = "OrionOrbShrapnel",
            AmmoRound = "OrionOrbShrapnel",
            HybridRound = false, //AmmoMagazine based weapon with energy cost
            EnergyCost = 0.00000000001f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
            BaseDamage = 1000f,
            Mass = 0f, // in kilograms
            Health = 0, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying.
            BackKickForce = 100f,
            DecayPerShot = 0f,
            HardPointUsable = false, // set to false if this is a Fragment ammoType and you don't want the turret to be able to select it directly.
            EnergyMagazineSize = 12, //ONLY USE IF "AmmoMagazine=ENERGY". Otherwise specify magazine size in Ammo.sbc file.
            IgnoreWater = false,

            Shape = new ShapeDef //defines the collision shape of projectile, defaults line and visual Line Length if set to 0
            {
                Shape = LineShape, // LineShape or SphereShape. Do not use SphereShape for fast moving projectiles if you care about precision.
                Diameter = 1, // Diameter is minimum length of LineShape or minimum diameter of SphereShape
            },
            ObjectsHit = new ObjectsHitDef
            {
                MaxObjectsHit = 2, // 0 = disabled
                CountBlocks = true, // counts gridBlocks and not just entities hit
            },
            Fragment = new FragmentDef
            {
                AmmoRound = "",
                Fragments = 0,
                Degrees = 15,
                Reverse = false,
                RandomizeDir = false, // randomize between forward and backward directions
            },
            Pattern = new PatternDef
            {
                Patterns = new[] {
                    "",
                },
                Enable = false,
                TriggerChance = 1f,
                Random = false,
                RandomMin = 1,
                RandomMax = 1,
                SkipParent = false,
                PatternSteps = 1, // Number of Patterns activated per round, will progress in order and loop.  Ignored if Random = true.
            },
            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0f, // 0 = disabled, 1000 = any blocks with currently integrity above 1000 will be immune to damage.
                DamageVoxels = false, // true = voxels are vulnerable to this weapon
                SelfDamage = false, // true = allow self damage.
                HealthHitModifier = 1, // defaults to a value of 1, this setting modifies how much Health is subtracted from a projectile per hit (1 = per hit).
                VoxelHitModifier = 10,
                Characters = 1f,
                // modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01 = 1% damage, 2 = 200% damage.
                FallOff = new FallOffDef
                {
                    Distance = 0f, // Distance at which max damage begins falling off.
                    MinMultipler = 0f, // value from 0.0f to 1f where 0.1f would be a min damage of 10% of max damage.
                },
                Grids = new GridSizeDef
                    {
                        Large = .5f,
                        Small = 1f,
                    },
                Armor = new ArmorDef
                {
                    Armor = .25f,
                    Light = .25f,
                    Heavy = .1f,
                    NonArmor = 1f,
                },
                Shields = new ShieldDef
                {
                    Modifier = .1f,
                    Type = Default,
                    BypassModifier = -1f,
                },
                // first true/false (ignoreOthers) will cause projectiles to pass through all blocks that do not match the custom subtypeIds.
                Custom = new CustomScalesDef
                {
                    IgnoreAllOthers = false,
                    Types = new[]
                    {
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test1",
                            Modifier = -1f,
                        },
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test2",
                            Modifier = -1f,
                        },
                    },
                },
            },
            AreaEffect = new AreaDamageDef
                {
                    AreaEffect = Explosive, // Disabled = do not use area effect at all, Explosive, Radiant, AntiSmart, JumpNullField, JumpNullField, EnergySinkField, AnchorField, EmpField, OffenseField, NavField, DotField.
                    AreaEffectDamage = 0f, // 0 = DO NOT USE FOR EXPLOSIVES, USE DETONATION SETTINGS INSTEAD
                    AreaEffectRadius = 0f, // DO NOT USE FOR EXPLOSIVES, USE DETONATION SETTINGS INSTEAD
                    Pulse = new PulseDef // interval measured in game ticks (60 == 1 second), pulseChance chance (0 - 100) that an entity in field will be hit
                    {
                        Interval = 30,
                        PulseChance = 0,
                    },
                    Explosions = new ExplosionDef
                    {
                        NoVisuals = false, //Set to false to use vanilla explosion particles, unless specified a custom particle
                        NoSound = false, ////Set to false to use vanilla explosion noises, unless specified a custom sound
                        Scale = 1f, //Scale of the particles.
                        CustomParticle = "PlasmaBolt",
                        CustomSound = "",
                    },
                    Detonation = new DetonateDef //USE THESE MODIFIERS FOR EXPLOSIONS
                    {
                        DetonateOnEnd = true,
                        ArmOnlyOnHit = false,
                        DetonationDamage = 60, //1 Steelplate =100HP, 1 large light armor cube=2500HP. Has a natural falloff based on radius.
                        DetonationRadius = 2, //Measured in meters, how large the explosion should be.
                    },
                    EwarFields = new EwarFieldsDef
                    {
                        Duration = 0,
                        StackDuration = true,
                        Depletable = false,
                        MaxStacks = 10,
                        TriggerRange = 5f,
                    },
                },
            Beams = new BeamDef
            {
                Enable = false,
                VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
                ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
                RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
                OneParticle = false, // Only spawn one particle hit per beam weapon.
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = None,
                TargetLossDegree = 80f,
                TargetLossTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                MaxLifeTime = 120, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                AccelPerSec = 0f,
                DesiredSpeed = 10, // DO NOT SET HIGHER THAN 4100
                MaxTrajectory = 3660f,
                FieldTime = 0, // 0 is disabled, a value causes the projectile to come to rest, spawn a field and remain for a time (Measured in game ticks, 60 = 1 second)
                GravityMultiplier = 1f, // Gravity multiplier, influences the trajectory of the projectile, value greater than 0 to enable.
                SpeedVariance = Random(start: 0, end: 0), // subtracts value from DesiredSpeed
                RangeVariance = Random(start: 0, end: 0), // subtracts value from MaxTrajectory
                MaxTrajectoryTime = 0, // How long the weapon must fire before it reaches MaxTrajectory.
                Smarts = new SmartsDef
                {
                    Inaccuracy = 5f, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
                    Aggressiveness = 1f, // controls how responsive tracking is.
                    MaxLateralThrust = 0.5, // controls how sharp the trajectile may turn
                    TrackingDelay = 0, // Measured in Shape diameter units traveled.
                    MaxChaseTime = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    OverideTarget = true, // when set to true ammo picks its own target, does not use hardpoint's.
                    MaxTargets = 0, // Number of targets allowed before ending, 0 = unlimited
                    NoTargetExpire = false, // Expire without ever having a target at TargetLossTime
                    Roam = false, // Roam current area after target loss
                    KeepAliveAfterTargetLoss = false, // Whether to stop early death of projectile on target loss
                },
                Mines = new MinesDef
                {
                    DetectRadius = 0,
                    DeCloakRadius = 0,
                    FieldTime = 0,
                    Cloak = false,
                    Persist = false,
                },
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "\\Models\\Akiad\\Small\\OrionGlobe.mwm",
                VisualProbability = 1f,
                ShieldHitDraw = false,
                Particles = new AmmoParticleDef
                {
                    Ammo = new ParticleDef
                    {
                        Name = "", //ShipWelderArc
                        ShrinkByDistance = false,
                        Color = Color(red: 128, green: 0, blue: 0, alpha: 32),
                        Offset = Vector(x: 0, y: -1, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 1,
                            Scale = 1,
                        },
                    },
                    Hit = new ParticleDef
                    {
                        Name = "",
                        ApplyToShield = true,
                        ShrinkByDistance = false,
                        Color = Color(red: 3, green: 1.9f, blue: 1f, alpha: 1),
                        Offset = Vector(x: 0, y: 0, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 30,
                            Scale = 1,
                            HitPlayChance = 1f,
                        },
                    },
                    Eject = new ParticleDef
                    {
                        Name = "",
                        ApplyToShield = true,
                        ShrinkByDistance = false,
                        Color = Color(red: 3, green: 1.9f, blue: 1f, alpha: 1),
                        Offset = Vector(x: 0, y: 0, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 30,
                            Scale = 1,
                            HitPlayChance = 1f,
                        },
                    },
                },
                Lines = new LineDef
                {
                    ColorVariance = Random(start: 0.75f, end: 2f), // multiply the color by random values within range.
                    WidthVariance = Random(start: 0f, end: 0f), // adds random value to default width (negatives shrinks width)
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = 1f,
                        Width = 0.2f,
                        Color = Color(red: 3, green: 2, blue: 1f, alpha: 1),
                        VisualFadeStart = 0, // Number of ticks the weapon has been firing before projectiles begin to fade their color
                        VisualFadeEnd = 0, // How many ticks after fade began before it will be invisible.
                        Textures = new[] {// WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                            "ProjectileTrailLine",
                        },
                        TextureMode = Normal, // Normal, Cycle, Chaos, Wave
                        Segmentation = new SegmentDef
                        {
                            Enable = false, // If true Tracer TextureMode is ignored
                            Textures = new[] {
								"",
                            },
                            SegmentLength = 0f, // Uses the values below.
                            SegmentGap = 0f, // Uses Tracer textures and values
                            Speed = 1f, // meters per second
                            Color = Color(red: 1, green: 2, blue: 2.5f, alpha: 1),
                            WidthMultiplier = 1f,
                            Reverse = false,
                            UseLineVariance = true,
                            WidthVariance = Random(start: 0f, end: 0f),
                            ColorVariance = Random(start: 0f, end: 0f)
                        }
                    },
                    Trail = new TrailDef
                    {
                        Enable = false,
                        Textures = new[] {
							"",
                        },
                        TextureMode = Normal,
                        DecayTime = 128,
                        Color = Color(red: 0, green: 0, blue: 1, alpha: 1),
                        Back = false,
                        CustomWidth = 0,
                        UseWidthVariance = false,
                        UseColorFade = true,
                    },
                    OffsetEffect = new OffsetEffectDef
                    {
                        MaxOffset = 0,// 0 offset value disables this effect
                        MinLength = 0.2f,
                        MaxLength = 3,
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                TravelSound = "",
                HitSound = "",
                ShieldHitSound = "",
                PlayerHitSound = "",
                VoxelHitSound = "",
                FloatingHitSound = "",
                HitPlayChance = 0.5f,
                HitPlayShield = true,
            }, // Don't edit below this line
            Ejection = new EjectionDef
            {
                Type = Particle, // Particle or Item (Inventory Component)
                Speed = 100f, // Speed inventory is ejected from in dummy direction
                SpawnChance = 0.5f, // chance of triggering effect (0 - 1)
                CompDef = new ComponentDef
                {
                    ItemName = "", //InventoryComponent name
                    ItemLifeTime = 0, // how long item should exist in world
                    Delay = 0, // delay in ticks after shot before ejected
                }
            },
        };
		
		private AmmoDef ThresherMIRV => new AmmoDef
        {
            AmmoMagazine = "ThresherMIRV", //Name of your ammo type definied in your ammo.sbc file.
                AmmoRound = "MIRV", //Desired model for the projectiles, must be defined in your ammo.sbc file.
                HybridRound = false, //AmmoMagazine based weapon with energy cost
                EnergyCost = 0.00000000001f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
                BaseDamage = 1000f, //Think of this as level of penetration, low base damage for no penetration. Every Block destroyed takes their HP off the projectile.
                Mass = 10f, // in kilograms, determines the amount of kinetic energy applied to the grid being hit.
                Health = 1, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying. KEEP THIS LOW. 1=Standard,10=Insane
                BackKickForce = 1000f, //Determines the amount of kinetic energy applied to the grid firing.
				DecayPerShot = 0f,
				HardPointUsable = true, // set to false if this is a Fragment ammoType and you don't want the turret to be able to select it directly.
				EnergyMagazineSize = 12, //ONLY USE IF "AmmoMagazine=ENERGY". Otherwise specify magazine size in Ammo.sbc file.
				IgnoreWater = false,

                Shape = new ShapeDef //defines the collision shape of projectile, defaults line and visual Line Length if set to 0
                {
                    Shape = LineShape,
                    Diameter = 1,
                },
                ObjectsHit = new ObjectsHitDef
                {
                    MaxObjectsHit = 0, // 0 = disabled
                    CountBlocks = false, // counts gridBlocks and not just entities hit
                },
                Fragment = new FragmentDef
				{
					AmmoRound = "SmartBomb",
					Fragments = 35, //Number of Fragment projectiles created on impact.
					Degrees = 75,  //Think of this as a cone in front of the projectile, 360 degrees for a full circle.
					Reverse = true, //Redirects the cone back towards the projectile's rear
					RandomizeDir = false,
				},
                DamageScales = new DamageScaleDef
                {
                    MaxIntegrity = 0f, // 0 = disabled, 1000 = any blocks with currently integrity above 1000 will be immune to damage.
                    DamageVoxels = false, // true = voxels are vulnerable to this weapon
                    SelfDamage = false, // true = allow self damage.
                    HealthHitModifier = 2, // defaults to a value of 1, this setting modifies how much Health is subtracted from a projectile per hit (1 = per hit).
                    VoxelHitModifier = 3,  // modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01 = 1% damage, 2 = 200% damage.
                    Characters = 0.2f, //Damage to characters. 1f=same as damage to grids
                    Grids = new GridSizeDef
                    {
                        Large = 5f,
                        Small = 1f,
                    },
                    Armor = new ArmorDef
                    {
                        Armor = 1f,
                        Light = 1f,
                        Heavy = 1f,
                        NonArmor = 1f,
                    },
                    Shields = new ShieldDef
                    {
                        Modifier = .5f,
                        Type = Default,
                        BypassModifier = -1f,
                    },
                    // first true/false (ignoreOthers) will cause projectiles to pass through all blocks that do not match the custom subtypeIds.
                    Custom = new CustomScalesDef
                    {
                        IgnoreAllOthers = false,
                        Types = new []
                        {
                            new CustomBlocksDef
                            {
                                SubTypeId = "Test1",
                                Modifier = -1f,
                            },
                            new CustomBlocksDef
                            {
                                SubTypeId = "Test2",
                                Modifier = -1f,
                            },
                        },
                    },
                },
                AreaEffect = new AreaDamageDef
                {
                    AreaEffect = Explosive, // Disabled = do not use area effect at all, Explosive, Radiant, AntiSmart, JumpNullField, JumpNullField, EnergySinkField, AnchorField, EmpField, OffenseField, NavField, DotField.
                    AreaEffectDamage = 0f, // 0 = DO NOT USE FOR EXPLOSIVES, USE DETONATION SETTINGS INSTEAD
                    AreaEffectRadius = 0f, // DO NOT USE FOR EXPLOSIVES, USE DETONATION SETTINGS INSTEAD
                    Pulse = new PulseDef // interval measured in game ticks (60 == 1 second), pulseChance chance (0 - 100) that an entity in field will be hit
                    {
                        Interval = 30,
                        PulseChance = 0,
                    },
                    Explosions = new ExplosionDef
                    {
                        NoVisuals = false, //Set to false to use vanilla explosion particles, unless specified a custom particle
                        NoSound = false, ////Set to false to use vanilla explosion noises, unless specified a custom sound
                        Scale = .1f, //Scale of the particles.
                        CustomParticle = "",
                        CustomSound = "",
                    },
                    Detonation = new DetonateDef //USE THESE MODIFIERS FOR EXPLOSIONS
                    {
                        DetonateOnEnd = false,
                        ArmOnlyOnHit = false,
                        DetonationDamage = 0, //1 Steelplate =100HP, 1 large light armor cube=2500HP. Has a natural falloff based on radius.
                        DetonationRadius = 1, //Measured in meters, how large the explosion should be.
                    },
                    EwarFields = new EwarFieldsDef
                    {
                        Duration = 0,
                        StackDuration = true,
                        Depletable = false,
                        MaxStacks = 10,
                        TriggerRange = 5f,
                    },
                },
                Beams = new BeamDef
                {
                    Enable = false,
                    VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
                    ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
                    RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
                    OneParticle = false, // Only spawn one particle hit per beam weapon.
                },
                Trajectory = new TrajectoryDef
				{
					Guidance = Smart, //ammo guidance type, options are None, Smart, Remote(not used yet), TravelTo(if tracking travels to target Position, if none tracking travels to max trajectory, good for flak), DetectFixed(mines), DetectSmart(mines), DetectTravelTo(mines)
					TargetLossDegree = 180f,
					TargetLossTime = 90, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
					MaxLifeTime = 120, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
					AccelPerSec = 100,
					DesiredSpeed = 1000,
					MaxTrajectory = 5000f,
					FieldTime = 0, // 0 is disabled, a value causes the projectile to come to rest, spawn a field and remain for a time (Measured in game ticks, 60 = 1 second)
					GravityMultiplier = 0f, // Gravity influences the trajectory of the projectile.
					SpeedVariance = Random(start: 0, end: 0), // subtracts value from DesiredSpeed
					RangeVariance = Random(start: 0, end: 0), // subtracts value from MaxTrajectory
					Smarts = new SmartsDef
					{
						Inaccuracy = 2f, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
						Aggressiveness = 3f, // controls how responsive tracking is.
						MaxLateralThrust = 0.5, // controls how sharp the trajectile may turn
						TrackingDelay = 200, // Measured in Shape diameter units traveled.
						MaxChaseTime = 2400, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
						OverideTarget = false, // when set to true ammo picks its own target, does not use hardpoint's.
						MaxTargets = 1,
					},
					Mines = new MinesDef
					{
						DetectRadius = 200,
						DeCloakRadius = 100,
						FieldTime = 0,
						Cloak = true,
						Persist = false,
					},
				},
                AmmoGraphics = new GraphicDef
                {
                    ModelName = "\\Models\\Akiad\\Small\\ThresherMissileLaunched.mwm", //USE DOUBLE LINES, ex: "\\Models\\Name\\Small\\MyGun.mwm"
                    VisualProbability = 1f, //Percentage chance for the model to spawn, "1f" for 100% chance.
                    ShieldHitDraw = true,
                    Particles = new AmmoParticleDef
                    {
                        Ammo = new ParticleDef
                        {
                            Name = "", //Particle that surrounds your projectile. ShipWelderArc
                            Color = Color(red: 1f, green: 1f, blue: 1f, alpha: 1),
                            Offset = Vector(x: 0, y: 0, z: 0),
                            Extras = new ParticleOptionDef
                            {
                                Loop = true,
                                Restart = false,
                                MaxDistance = 5000,
                                MaxDuration = 1,
                                Scale = 0.25f,
                            },
                        },
                        Hit = new ParticleDef
                        {
                            Name = "",
                            ApplyToShield = true,
                            ShrinkByDistance = true,
                            Color = Color(red: 3, green: 2, blue: 1, alpha: 1),
                            Offset = Vector(x: 0, y: 0, z: 0),
                            Extras = new ParticleOptionDef
                            {
                                Loop = false,
                                Restart = false,
                                MaxDistance = 500,
                                MaxDuration = 1,
                                Scale = .25f,
                                HitPlayChance = 1f,
                            },
                        },
                    },
                    Lines = new LineDef
                    {
                        TracerMaterial = "ProjectileTrailLine", // Particle that follows your projectile. WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                        ColorVariance = Random(start: 5f, end: 10f), // multiply the color by random values within range.
                        WidthVariance = Random(start: 0f, end: 0.045f), // adds random value to default width (negatives shrinks width)
                        Tracer = new TracerBaseDef
                        {
                            Enable = true,
                            Length = .001f,
                            Width = 0.005f,
                            Color = Color(red: 0.9f, green: 0.9f, blue: 0.9f, alpha: 1),
                        },
                        Trail = new TrailDef
                        {
                            Enable = true,
                            Material = "WeaponLaser",
                            DecayTime = 8,
                            Color = Color(red: 50, green: 10, blue: 0, alpha: 1),
                            Back = true,
                            CustomWidth = 0,
                            UseWidthVariance = false,
                            UseColorFade = true,
                        },
                        OffsetEffect = new OffsetEffectDef
                        {
                            MaxOffset =  0,// 0 offset value disables this effect
                            MinLength = 0.2f,
                            MaxLength = 3,
                        },
                    },
                },
                AmmoAudio = new AmmoAudioDef
                {
                    TravelSound = "",
                    HitSound = "",
                    HitPlayChance = 0.1f,
                    HitPlayShield = true,
                }, // Don't edit below this line

        };
		
		private AmmoDef ThresherMissile => new AmmoDef
        {
            AmmoMagazine = "ThresherMissile", //Name of your ammo type definied in your ammo.sbc file.
                AmmoRound = "Missile", //Desired model for the projectiles, must be defined in your ammo.sbc file.
                HybridRound = false, //AmmoMagazine based weapon with energy cost
                EnergyCost = 0.00001f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
                BaseDamage = 1f, //Think of this as level of penetration, low base damage for no penetration. Every Block destroyed takes their HP off the projectile.
                Mass = 100f, // in kilograms, determines the amount of kinetic energy applied to the grid being hit.
                Health = 1, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying. KEEP THIS LOW. 1=Standard,10=Insane
                BackKickForce = 1000f, //Determines the amount of kinetic energy applied to the grid firing.
				DecayPerShot = 0f,
				HardPointUsable = true, // set to false if this is a Fragment ammoType and you don't want the turret to be able to select it directly.
				EnergyMagazineSize = 12, //ONLY USE IF "AmmoMagazine=ENERGY". Otherwise specify magazine size in Ammo.sbc file.
				IgnoreWater = false,

                Shape = new ShapeDef //defines the collision shape of projectile, defaults line and visual Line Length if set to 0
                {
                    Shape = LineShape,
                    Diameter = 1,
                },
                ObjectsHit = new ObjectsHitDef
                {
                    MaxObjectsHit = 0, // 0 = disabled
                    CountBlocks = false, // counts gridBlocks and not just entities hit
                },
                Fragment = new FragmentDef
				{
					AmmoRound = "HEShrapnel",
					Fragments = 10, //Number of Fragment projectiles created on impact.
					Degrees = 180,  //Think of this as a cone in front of the projectile, 360 degrees for a full circle.
					Reverse = false, //Redirects the cone back towards the projectile's rear
					RandomizeDir = false,
				},
                DamageScales = new DamageScaleDef
                {
                    MaxIntegrity = 0f, // 0 = disabled, 1000 = any blocks with currently integrity above 1000 will be immune to damage.
                    DamageVoxels = false, // true = voxels are vulnerable to this weapon
                    SelfDamage = false, // true = allow self damage.
                    HealthHitModifier = 1, // defaults to a value of 1, this setting modifies how much Health is subtracted from a projectile per hit (1 = per hit).
                    VoxelHitModifier = 3,  // modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01 = 1% damage, 2 = 200% damage.
                    Characters = 0.2f, //Damage to characters. 1f=same as damage to grids
                    Grids = new GridSizeDef
                    {
                        Large = 1f,
                        Small = 1f,
                    },
                    Armor = new ArmorDef
                    {
                        Armor = 1f,
                        Light = 1f,
                        Heavy = 1f,
                        NonArmor = 1f,
                    },
                    Shields = new ShieldDef
                    {
                        Modifier = .75f,
                        Type = Default,
                        BypassModifier = -1f,
                    },
                    // first true/false (ignoreOthers) will cause projectiles to pass through all blocks that do not match the custom subtypeIds.
                    Custom = new CustomScalesDef
                    {
                        IgnoreAllOthers = false,
                        Types = new []
                        {
                            new CustomBlocksDef
                            {
                                SubTypeId = "Test1",
                                Modifier = -1f,
                            },
                            new CustomBlocksDef
                            {
                                SubTypeId = "Test2",
                                Modifier = -1f,
                            },
                        },
                    },
                },
                AreaEffect = new AreaDamageDef
                {
                    AreaEffect = Explosive, // Disabled = do not use area effect at all, Explosive, Radiant, AntiSmart, JumpNullField, JumpNullField, EnergySinkField, AnchorField, EmpField, OffenseField, NavField, DotField.
                    AreaEffectDamage = 0f, // 0 = DO NOT USE FOR EXPLOSIVES, USE DETONATION SETTINGS INSTEAD
                    AreaEffectRadius = 0f, // DO NOT USE FOR EXPLOSIVES, USE DETONATION SETTINGS INSTEAD
                    Pulse = new PulseDef // interval measured in game ticks (60 == 1 second), pulseChance chance (0 - 100) that an entity in field will be hit
                    {
                        Interval = 30,
                        PulseChance = 0,
                    },
                    Explosions = new ExplosionDef
                    {
                        NoVisuals = false, //Set to false to use vanilla explosion particles, unless specified a custom particle
                        NoSound = false, ////Set to false to use vanilla explosion noises, unless specified a custom sound
                        Scale = 1f, //Scale of the particles.
                        CustomParticle = "",
                        CustomSound = "",
                    },
                    Detonation = new DetonateDef //USE THESE MODIFIERS FOR EXPLOSIONS
                    {
                        DetonateOnEnd = true,
                        ArmOnlyOnHit = false,
                        DetonationDamage = 3000, //1 Steelplate =100HP, 1 large light armor cube=2500HP. Has a natural falloff based on radius.
                        DetonationRadius = 6, //Measured in meters, how large the explosion should be.
                    },
                    EwarFields = new EwarFieldsDef
                    {
                        Duration = 0,
                        StackDuration = true,
                        Depletable = false,
                        MaxStacks = 10,
                        TriggerRange = 5f,
                    },
                },
                Beams = new BeamDef
                {
                    Enable = false,
                    VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
                    ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
                    RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
                    OneParticle = false, // Only spawn one particle hit per beam weapon.
                },
                Trajectory = new TrajectoryDef
				{
					Guidance = Smart, //ammo guidance type, options are None, Smart, Remote(not used yet), TravelTo(if tracking travels to target Position, if none tracking travels to max trajectory, good for flak), DetectFixed(mines), DetectSmart(mines), DetectTravelTo(mines)
					TargetLossDegree = 180f,
					TargetLossTime = 90, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
					MaxLifeTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
					AccelPerSec = 100,
					DesiredSpeed = 1000,
					MaxTrajectory = 5000f,
					FieldTime = 0, // 0 is disabled, a value causes the projectile to come to rest, spawn a field and remain for a time (Measured in game ticks, 60 = 1 second)
					GravityMultiplier = 0f, // Gravity influences the trajectory of the projectile.
					SpeedVariance = Random(start: 0, end: 0), // subtracts value from DesiredSpeed
					RangeVariance = Random(start: 0, end: 0), // subtracts value from MaxTrajectory
					Smarts = new SmartsDef
					{
						Inaccuracy = 2f, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
						Aggressiveness = 3f, // controls how responsive tracking is.
						MaxLateralThrust = 0.5, // controls how sharp the trajectile may turn
						TrackingDelay = 200, // Measured in Shape diameter units traveled.
						MaxChaseTime = 2400, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
						OverideTarget = false, // when set to true ammo picks its own target, does not use hardpoint's.
						MaxTargets = 1,
					},
					Mines = new MinesDef
					{
						DetectRadius = 200,
						DeCloakRadius = 100,
						FieldTime = 0,
						Cloak = true,
						Persist = false,
					},
				},
                AmmoGraphics = new GraphicDef
                {
                    ModelName = "\\Models\\Akiad\\Small\\ThresherMissileLaunched.mwm", //USE DOUBLE LINES, ex: "\\Models\\Name\\Small\\MyGun.mwm"
                    VisualProbability = 1f, //Percentage chance for the model to spawn, "1f" for 100% chance.
                    ShieldHitDraw = true,
                    Particles = new AmmoParticleDef
                    {
                        Ammo = new ParticleDef
                        {
                            Name = "", //Particle that surrounds your projectile. ShipWelderArc
                            Color = Color(red: 1f, green: 1f, blue: 1f, alpha: 1),
                            Offset = Vector(x: 0, y: 0, z: 0),
                            Extras = new ParticleOptionDef
                            {
                                Loop = true,
                                Restart = false,
                                MaxDistance = 5000,
                                MaxDuration = 1,
                                Scale = 0.25f,
                            },
                        },
                        Hit = new ParticleDef
                        {
                            Name = "",
                            ApplyToShield = true,
                            ShrinkByDistance = true,
                            Color = Color(red: 3, green: 2, blue: 1, alpha: 1),
                            Offset = Vector(x: 0, y: 0, z: 0),
                            Extras = new ParticleOptionDef
                            {
                                Loop = false,
                                Restart = false,
                                MaxDistance = 500,
                                MaxDuration = 1,
                                Scale = .25f,
                                HitPlayChance = 1f,
                            },
                        },
                    },
                    Lines = new LineDef
                    {
                        TracerMaterial = "ProjectileTrailLine", // Particle that follows your projectile. WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                        ColorVariance = Random(start: 5f, end: 10f), // multiply the color by random values within range.
                        WidthVariance = Random(start: 0f, end: 0.045f), // adds random value to default width (negatives shrinks width)
                        Tracer = new TracerBaseDef
                        {
                            Enable = true,
                            Length = .001f,
                            Width = 0.005f,
                            Color = Color(red: 0.9f, green: 0.9f, blue: 0.9f, alpha: 1),
                        },
                        Trail = new TrailDef
                        {
                            Enable = true,
                            Material = "WeaponLaser",
                            DecayTime = 8,
                            Color = Color(red: 0, green: 50, blue: 50, alpha: 1),
                            Back = true,
                            CustomWidth = 0,
                            UseWidthVariance = false,
                            UseColorFade = true,
                        },
                        OffsetEffect = new OffsetEffectDef
                        {
                            MaxOffset =  0,// 0 offset value disables this effect
                            MinLength = 0.2f,
                            MaxLength = 3,
                        },
                    },
                },
                AmmoAudio = new AmmoAudioDef
                {
                    TravelSound = "",
                    HitSound = "",
                    HitPlayChance = 0.1f,
                    HitPlayShield = true,
                }, // Don't edit below this line

        };
		
		private AmmoDef ThresherEMP => new AmmoDef
        {
				AmmoMagazine = "ThresherEMP", //Name of your ammo type definied in your ammo.sbc file.
                AmmoRound = "EMP", //Desired model for the projectiles, must be defined in your ammo.sbc file.
                HybridRound = false, //AmmoMagazine based weapon with energy cost
				EnergyCost = 0.5f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
				BaseDamage = 80000f,
				Mass = 220f, // in kilograms
				Health = 11, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying.
				BackKickForce = 60f,
				HardPointUsable = true, // set to false if this is a Fragment ammoType and you don't want the turret to be able to select it directly.

				Shape = new ShapeDef //defines the collision shape of projectile, defaults line and visual Line Length if set to 0
				{
					Shape = LineShape,
					Diameter = 1,
				},
				ObjectsHit = new ObjectsHitDef
				{
					MaxObjectsHit = 0, // 0 = disabled
					CountBlocks = false, // counts gridBlocks and not just entities hit
				},
				Fragment = new FragmentDef //FIX MANUALLY
				{
					AmmoRound = "",
					Fragments = 1,
					Degrees = 1,
					Reverse = false,
					RandomizeDir = true,
				},
				DamageScales = new DamageScaleDef
				{
					MaxIntegrity = 0f, // 0 = disabled, 1000 = any blocks with currently integrity above 1000 will be immune to damage.
					DamageVoxels = false, // true = voxels are vulnerable to this weapon
					SelfDamage = false, // true = allow self damage.

					 FallOff = new FallOffDef
					{
						Distance = 5000f, // Distance at which max damage begins falling off.
						MinMultipler = 1f, // value from 0.0f to 1f where 0.1f is equal to a min damage of 10% of max damage.
					},
					
					// modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01 = 1% damage, 2 = 200% damage.
					Characters = -1f,
					Grids = new GridSizeDef
                    {
                        Large = 1f,
                        Small = 1f,
                    },
					Armor = new ArmorDef
					{
						Armor = 1f,
						Light = 1f,
						Heavy = 0.25f,
						NonArmor = 2.25f,
					},
					Shields = new ShieldDef
					{
						Modifier = 0.02f,
						Type = Default,
						BypassModifier = 0.5f
										},
					// first true/false (ignoreOthers) will cause projectiles to pass through all blocks that do not match the custom subtypeIds.
					Custom = new CustomScalesDef
					{
						IgnoreAllOthers = false,
						Types = new[]
						{
							new CustomBlocksDef
							{
								SubTypeId = "Test1",
								Modifier = -1f,
							},
							new CustomBlocksDef
							{
								SubTypeId = "Test2",
								Modifier = -1f,
							},
						},
					},
				},
					AreaEffect = new AreaDamageDef
				{
					AreaEffect = EmpField, // Disabled = do not use area effect at all, Explosive, Radiant, AntiSmart, JumpNullField, JumpNullField, EnergySinkField, AnchorField, EmpField, OffenseField, NavField, DotField.
					AreaEffectDamage = 100000f, // 0 = use spillover from BaseDamage, otherwise use this value.
					AreaEffectRadius = 50f,
					Pulse = new PulseDef // interval measured in game ticks (60 == 1 second), pulseChance chance (0 - 100) that an entity in field will be hit
					{
						Interval = 20,
						PulseChance = 100,
						
					},
					Explosions = new ExplosionDef
					{
						NoVisuals = false,
						NoSound = false,
						Scale = 1,
						CustomParticle = "",
						CustomSound = "",
					},
					Detonation = new DetonateDef
					{
						DetonateOnEnd = true,
						ArmOnlyOnHit = false,
						DetonationDamage = 0,
						DetonationRadius = 0,
					},
					EwarFields = new EwarFieldsDef //FIX Manually
					{
						Duration = 180,
						StackDuration = true,
						Depletable = false,
						MaxStacks = 20,
						TriggerRange = 30f,
					},
				},
				Beams = new BeamDef
				{
					Enable = false,
					VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
					ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
					RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
					OneParticle = false, // Only spawn one particle hit per beam weapon.
				},
				 Trajectory = new TrajectoryDef
				{
					Guidance = Smart, //ammo guidance type, options are None, Smart, Remote(not used yet), TravelTo(if tracking travels to target Position, if none tracking travels to max trajectory, good for flak), DetectFixed(mines), DetectSmart(mines), DetectTravelTo(mines)
					TargetLossDegree = 180f,
					TargetLossTime = 90, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
					MaxLifeTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
					AccelPerSec = 100,
					DesiredSpeed = 1000,
					MaxTrajectory = 5000f,
					FieldTime = 180, // 0 is disabled, a value causes the projectile to come to rest, spawn a field and remain for a time (Measured in game ticks, 60 = 1 second)
					GravityMultiplier = 0f, // Gravity influences the trajectory of the projectile.
					SpeedVariance = Random(start: 0, end: 0), // subtracts value from DesiredSpeed
					RangeVariance = Random(start: 0, end: 0), // subtracts value from MaxTrajectory
					Smarts = new SmartsDef
					{
						Inaccuracy = 2f, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
						Aggressiveness = 3f, // controls how responsive tracking is.
						MaxLateralThrust = 0.5, // controls how sharp the trajectile may turn
						TrackingDelay = 200, // Measured in Shape diameter units traveled.
						MaxChaseTime = 2400, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
						OverideTarget = false, // when set to true ammo picks its own target, does not use hardpoint's.
						MaxTargets = 1,
					},
					Mines = new MinesDef
					{
						DetectRadius = 200,
						DeCloakRadius = 100,
						FieldTime = 0,
						Cloak = true,
						Persist = false,
					},
				},
                AmmoGraphics = new GraphicDef
                {
                    ModelName = "\\Models\\Akiad\\Small\\ThresherMissileLaunched.mwm", //USE DOUBLE LINES, ex: "\\Models\\Name\\Small\\MyGun.mwm"
                    VisualProbability = 1f, //Percentage chance for the model to spawn, "1f" for 100% chance.
                    ShieldHitDraw = true,
                    Particles = new AmmoParticleDef
                    {
                        Ammo = new ParticleDef
                        {
                            Name = "", //Particle that surrounds your projectile. ShipWelderArc
                            Color = Color(red: 1f, green: 1f, blue: 1f, alpha: 1),
                            Offset = Vector(x: 0, y: 0, z: 0),
                            Extras = new ParticleOptionDef
                            {
                                Loop = true,
                                Restart = false,
                                MaxDistance = 5000,
                                MaxDuration = 1,
                                Scale = 0.25f,
                            },
                        },
                        Hit = new ParticleDef
                        {
                            Name = "EMP",
                            ApplyToShield = true,
                            ShrinkByDistance = true,
                            Color = Color(red: 3, green: 2, blue: 1, alpha: 1),
                            Offset = Vector(x: 0, y: 0, z: 0),
                            Extras = new ParticleOptionDef
                            {
                                Loop = false,
                                Restart = false,
                                MaxDistance = 500,
                                MaxDuration = 1,
                                Scale = 1f,
                                HitPlayChance = 1f,
                            },
                        },
                    },
                    Lines = new LineDef
                    {
                        TracerMaterial = "ProjectileTrailLine", // Particle that follows your projectile. WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                        ColorVariance = Random(start: 5f, end: 10f), // multiply the color by random values within range.
                        WidthVariance = Random(start: 0f, end: 0.045f), // adds random value to default width (negatives shrinks width)
                        Tracer = new TracerBaseDef
                        {
                            Enable = true,
                            Length = .001f,
                            Width = 0.005f,
                            Color = Color(red: 0.9f, green: 0.9f, blue: 0.9f, alpha: 1),
                        },
                        Trail = new TrailDef
                        {
                            Enable = true,
                            Material = "WeaponLaser",
                            DecayTime = 8,
                            Color = Color(red: 0, green: 20, blue: 1000, alpha: 1),
                            Back = true,
                            CustomWidth = 0,
                            UseWidthVariance = false,
                            UseColorFade = true,
                        },
                        OffsetEffect = new OffsetEffectDef
                        {
                            MaxOffset =  0,// 0 offset value disables this effect
                            MinLength = 0.2f,
                            MaxLength = 3,
                        },
                    },
                },
                AmmoAudio = new AmmoAudioDef
                {
                    TravelSound = "",
                    HitSound = "",
                    HitPlayChance = 0.1f,
                    HitPlayShield = true,
                }, // Don't edit below this line

        };
		
		
		private AmmoDef ThresherStunLock => new AmmoDef
        {
				AmmoMagazine = "ThresherStunLock", //Name of your ammo type definied in your ammo.sbc file.
                AmmoRound = "StunLock", //Desired model for the projectiles, must be defined in your ammo.sbc file.
                HybridRound = false, //AmmoMagazine based weapon with energy cost
				EnergyCost = 0.5f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
				BaseDamage = 20000f,
				Mass = 220f, // in kilograms
				Health = 11, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying.
				BackKickForce = 60f,
				HardPointUsable = true, // set to false if this is a Fragment ammoType and you don't want the turret to be able to select it directly.

				Shape = new ShapeDef //defines the collision shape of projectile, defaults line and visual Line Length if set to 0
				{
					Shape = LineShape,
					Diameter = 1,
				},
				ObjectsHit = new ObjectsHitDef
				{
					MaxObjectsHit = 0, // 0 = disabled
					CountBlocks = false, // counts gridBlocks and not just entities hit
				},
				Fragment = new FragmentDef //FIX MANUALLY
				{
					AmmoRound = "",
					Fragments = 0,
					Degrees = 0,
					Reverse = false,
					RandomizeDir = true,
				},
				DamageScales = new DamageScaleDef
				{
					MaxIntegrity = 0f, // 0 = disabled, 1000 = any blocks with currently integrity above 1000 will be immune to damage.
					DamageVoxels = false, // true = voxels are vulnerable to this weapon
					SelfDamage = false, // true = allow self damage.

					 FallOff = new FallOffDef
					{
						Distance = 5000f, // Distance at which max damage begins falling off.
						MinMultipler = 1f, // value from 0.0f to 1f where 0.1f is equal to a min damage of 10% of max damage.
					},
					
					// modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01 = 1% damage, 2 = 200% damage.
					Characters = -1f,
					Grids = new GridSizeDef
                    {
                        Large = 1f,
                        Small = 1f,
                    },
					Armor = new ArmorDef
					{
						Armor = 1f,
						Light = 1f,
						Heavy = 0.25f,
						NonArmor = 2.25f,
					},
					Shields = new ShieldDef
					{
						Modifier = 0.02f,
						Type = Default,
						BypassModifier = 0.5f
										},
					// first true/false (ignoreOthers) will cause projectiles to pass through all blocks that do not match the custom subtypeIds.
					Custom = new CustomScalesDef
					{
						IgnoreAllOthers = false,
						Types = new[]
						{
							new CustomBlocksDef
							{
								SubTypeId = "Test1",
								Modifier = -1f,
							},
							new CustomBlocksDef
							{
								SubTypeId = "Test2",
								Modifier = -1f,
							},
						},
					},
				},
					AreaEffect = new AreaDamageDef
				{
					AreaEffect = EmpField, // Disabled = do not use area effect at all, Explosive, Radiant, AntiSmart, JumpNullField, JumpNullField, EnergySinkField, AnchorField, EmpField, OffenseField, NavField, DotField.
					AreaEffectDamage = 100000f, // 0 = use spillover from BaseDamage, otherwise use this value.
					AreaEffectRadius = 50f,
					Pulse = new PulseDef // interval measured in game ticks (60 == 1 second), pulseChance chance (0 - 100) that an entity in field will be hit
					{
						Interval = 20,
						PulseChance = 100,
						
					},
					Explosions = new ExplosionDef
					{
						NoVisuals = false,
						NoSound = false,
						Scale = 1,
						CustomParticle = "",
						CustomSound = "",
					},
					Detonation = new DetonateDef
					{
						DetonateOnEnd = true,
						ArmOnlyOnHit = false,
						DetonationDamage = 0,
						DetonationRadius = 0,
					},
					EwarFields = new EwarFieldsDef //FIX Manually
					{
						Duration = 240,
						StackDuration = true,
						Depletable = false,
						MaxStacks = 20,
						TriggerRange = 30f,
					},
				},
				Beams = new BeamDef
				{
					Enable = false,
					VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
					ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
					RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
					OneParticle = false, // Only spawn one particle hit per beam weapon.
				},
				 Trajectory = new TrajectoryDef
				{
					Guidance = Smart, //ammo guidance type, options are None, Smart, Remote(not used yet), TravelTo(if tracking travels to target Position, if none tracking travels to max trajectory, good for flak), DetectFixed(mines), DetectSmart(mines), DetectTravelTo(mines)
					TargetLossDegree = 180f,
					TargetLossTime = 90, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
					MaxLifeTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
					AccelPerSec = 100,
					DesiredSpeed = 1000,
					MaxTrajectory = 5000f,
					FieldTime = 240, // 0 is disabled, a value causes the projectile to come to rest, spawn a field and remain for a time (Measured in game ticks, 60 = 1 second)
					GravityMultiplier = 0f, // Gravity influences the trajectory of the projectile.
					SpeedVariance = Random(start: 0, end: 0), // subtracts value from DesiredSpeed
					RangeVariance = Random(start: 0, end: 0), // subtracts value from MaxTrajectory
					Smarts = new SmartsDef
					{
						Inaccuracy = 2f, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
						Aggressiveness = 3f, // controls how responsive tracking is.
						MaxLateralThrust = 0.5, // controls how sharp the trajectile may turn
						TrackingDelay = 200, // Measured in Shape diameter units traveled.
						MaxChaseTime = 2400, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
						OverideTarget = false, // when set to true ammo picks its own target, does not use hardpoint's.
						MaxTargets = 1,
					},
					Mines = new MinesDef
					{
						DetectRadius = 200,
						DeCloakRadius = 100,
						FieldTime = 0,
						Cloak = true,
						Persist = false,
					},
				},
                AmmoGraphics = new GraphicDef
                {
                    ModelName = "\\Models\\Akiad\\Small\\ThresherMissileLaunched.mwm", //USE DOUBLE LINES, ex: "\\Models\\Name\\Small\\MyGun.mwm"
                    VisualProbability = 1f, //Percentage chance for the model to spawn, "1f" for 100% chance.
                    ShieldHitDraw = true,
                    Particles = new AmmoParticleDef
                    {
                        Ammo = new ParticleDef
                        {
                            Name = "", //Particle that surrounds your projectile. ShipWelderArc
                            Color = Color(red: 1f, green: 1f, blue: 1f, alpha: 1),
                            Offset = Vector(x: 0, y: 0, z: 0),
                            Extras = new ParticleOptionDef
                            {
                                Loop = true,
                                Restart = false,
                                MaxDistance = 5000,
                                MaxDuration = 1,
                                Scale = 0.25f,
                            },
                        },
                        Hit = new ParticleDef
                        {
                            Name = "EMP",
                            ApplyToShield = true,
                            ShrinkByDistance = true,
                            Color = Color(red: 3, green: 2, blue: 1, alpha: 1),
                            Offset = Vector(x: 0, y: 0, z: 0),
                            Extras = new ParticleOptionDef
                            {
                                Loop = false,
                                Restart = false,
                                MaxDistance = 500,
                                MaxDuration = 1,
                                Scale = 1f,
                                HitPlayChance = 1f,
                            },
                        },
                    },
                    Lines = new LineDef
                    {
                        TracerMaterial = "ProjectileTrailLine", // Particle that follows your projectile. WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                        ColorVariance = Random(start: 5f, end: 10f), // multiply the color by random values within range.
                        WidthVariance = Random(start: 0f, end: 0.045f), // adds random value to default width (negatives shrinks width)
                        Tracer = new TracerBaseDef
                        {
                            Enable = true,
                            Length = .001f,
                            Width = 0.005f,
                            Color = Color(red: 0.9f, green: 0.9f, blue: 0.9f, alpha: 1),
                        },
                        Trail = new TrailDef
                        {
                            Enable = true,
                            Material = "WeaponLaser",
                            DecayTime = 8,
                            Color = Color(red: 115f, green: 90f, blue: 50f, alpha: 1),
                            Back = true,
                            CustomWidth = 0,
                            UseWidthVariance = false,
                            UseColorFade = true,
                        },
                        OffsetEffect = new OffsetEffectDef
                        {
                            MaxOffset =  0,// 0 offset value disables this effect
                            MinLength = 0.2f,
                            MaxLength = 3,
                        },
                    },
                },
                AmmoAudio = new AmmoAudioDef
                {
                    TravelSound = "",
                    HitSound = "",
                    HitPlayChance = 0.1f,
                    HitPlayShield = true,
                }, // Don't edit below this line

        };
		private AmmoDef CrusaderMissile1 => new AmmoDef
        {
				AmmoMagazine = "CrusaderMissile", //Name of your ammo type definied in your ammo.sbc file.
                AmmoRound = "CrusaderMissile1", //Desired model for the projectiles, must be defined in your ammo.sbc file.
                HybridRound = false, //AmmoMagazine based weapon with energy cost
                EnergyCost = 0.000001f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
                BaseDamage = 1f, //Think of this as level of penetration, low base damage for no penetration. Every Block destroyed takes their HP off the projectile.
                Mass = 100f, // in kilograms, determines the amount of kinetic energy applied to the grid being hit.
                Health = 1, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying. KEEP THIS LOW. 1=Standard,10=Insane
                BackKickForce = 1000f, //Determines the amount of kinetic energy applied to the grid firing.
				DecayPerShot = 0f,
				HardPointUsable = true, // set to false if this is a Fragment ammoType and you don't want the turret to be able to select it directly.
				EnergyMagazineSize = 12, //ONLY USE IF "AmmoMagazine=ENERGY". Otherwise specify magazine size in Ammo.sbc file.
				IgnoreWater = false,

                Shape = new ShapeDef //defines the collision shape of projectile, defaults line and visual Line Length if set to 0
                {
                    Shape = LineShape,
                    Diameter = 1,
                },
                ObjectsHit = new ObjectsHitDef
                {
                    MaxObjectsHit = 0, // 0 = disabled
                    CountBlocks = false, // counts gridBlocks and not just entities hit
                },
                Fragment = new FragmentDef
				{
					AmmoRound = "CrusaderMissile",
					Fragments = 1, //Number of Fragment projectiles created on impact.
					Degrees = 1,  //Think of this as a cone in front of the projectile, 360 degrees for a full circle.
					Reverse = false, //Redirects the cone back towards the projectile's rear
					RandomizeDir = false,
				},
                DamageScales = new DamageScaleDef
                {
                    MaxIntegrity = 0f, // 0 = disabled, 1000 = any blocks with currently integrity above 1000 will be immune to damage.
                    DamageVoxels = false, // true = voxels are vulnerable to this weapon
                    SelfDamage = false, // true = allow self damage.
                    HealthHitModifier = 3, // defaults to a value of 1, this setting modifies how much Health is subtracted from a projectile per hit (1 = per hit).
                    VoxelHitModifier = 3,  // modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01 = 1% damage, 2 = 200% damage.
                    Characters = 0.2f, //Damage to characters. 1f=same as damage to grids
                    Grids = new GridSizeDef
                    {
                        Large = 1f,
                        Small = 1f,
                    },
                    Armor = new ArmorDef
                    {
                        Armor = 1f,
                        Light = 1f,
                        Heavy = 1f,
                        NonArmor = 1f,
                    },
                    Shields = new ShieldDef
                    {
                        Modifier = .75f,
                        Type = Default,
                        BypassModifier = -1f,
                    },
                    // first true/false (ignoreOthers) will cause projectiles to pass through all blocks that do not match the custom subtypeIds.
                    Custom = new CustomScalesDef
                    {
                        IgnoreAllOthers = false,
                        Types = new []
                        {
                            new CustomBlocksDef
                            {
                                SubTypeId = "Test1",
                                Modifier = -1f,
                            },
                            new CustomBlocksDef
                            {
                                SubTypeId = "Test2",
                                Modifier = -1f,
                            },
                        },
                    },
                },
                AreaEffect = new AreaDamageDef
                {
                    AreaEffect = Disabled, // Disabled = do not use area effect at all, Explosive, Radiant, AntiSmart, JumpNullField, JumpNullField, EnergySinkField, AnchorField, EmpField, OffenseField, NavField, DotField.
                    AreaEffectDamage = 0f, // 0 = DO NOT USE FOR EXPLOSIVES, USE DETONATION SETTINGS INSTEAD
                    AreaEffectRadius = 0f, // DO NOT USE FOR EXPLOSIVES, USE DETONATION SETTINGS INSTEAD
                    Pulse = new PulseDef // interval measured in game ticks (60 == 1 second), pulseChance chance (0 - 100) that an entity in field will be hit
                    {
                        Interval = 30,
                        PulseChance = 0,
                    },
                    Explosions = new ExplosionDef
                    {
                        NoVisuals = false, //Set to false to use vanilla explosion particles, unless specified a custom particle
                        NoSound = false, ////Set to false to use vanilla explosion noises, unless specified a custom sound
                        Scale = 1f, //Scale of the particles.
                        CustomParticle = "",
                        CustomSound = "",
                    },
                    Detonation = new DetonateDef //USE THESE MODIFIERS FOR EXPLOSIONS
                    {
                        DetonateOnEnd = true,
                        ArmOnlyOnHit = false,
                        DetonationDamage = 3000, //1 Steelplate =100HP, 1 large light armor cube=2500HP. Has a natural falloff based on radius.
                        DetonationRadius = 6, //Measured in meters, how large the explosion should be.
                    },
                    EwarFields = new EwarFieldsDef
                    {
                        Duration = 0,
                        StackDuration = true,
                        Depletable = false,
                        MaxStacks = 10,
                        TriggerRange = 5f,
                    },
                },
                Beams = new BeamDef
                {
                    Enable = false,
                    VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
                    ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
                    RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
                    OneParticle = false, // Only spawn one particle hit per beam weapon.
                },
                Trajectory = new TrajectoryDef
                {
                    Guidance = Smart,
                    TargetLossDegree = 180f,
                    TargetLossTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    MaxLifeTime = 120, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    AccelPerSec = -80f,
                    DesiredSpeed = 100, //Speed of the projectile. DO NOT SET HIGHER THAN 4100.
                    MaxTrajectory = 6000f, //Maximum distance the projectile can travel before detonating/despawning.
                    FieldTime = 0, // 0 is disabled, a value causes the projectile to come to rest, spawn a field and remain for a time (Measured in game ticks, 60 = 1 second)
					GravityMultiplier = 0f, //1f=Gravity applies normal pull to projectile. 0 to disable.
                    SpeedVariance = Random(start: 0, end: 0), // subtracts value from DesiredSpeed
                    RangeVariance = Random(start: 0, end: 0), // subtracts value from MaxTrajectory
                    Smarts = new SmartsDef
                    {
                        Inaccuracy = 0, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
                        Aggressiveness = 3f, // controls how responsive tracking is.
                        MaxLateralThrust = .5f, // controls how sharp the trajectile may turn
                        TrackingDelay = 0, // Measured in Shape diameter units traveled.
                        MaxChaseTime = 2000, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                        OverideTarget = false, // when set to true ammo picks its own target, does not use hardpoint's.
                    },
                    Mines = new MinesDef
                    {
                        DetectRadius =  200,
                        DeCloakRadius = 100,
                        FieldTime = 1800,
                        Cloak = true,
                        Persist = false,
                    },
                },
                AmmoGraphics = new GraphicDef
                {
                    ModelName = "\\Models\\Akiad\\Small\\CrusaderMissile.mwm", //USE DOUBLE LINES, ex: "\\Models\\Name\\Small\\MyGun.mwm"
                    VisualProbability = 1f, //Percentage chance for the model to spawn, "1f" for 100% chance.
                    ShieldHitDraw = true,
                    Particles = new AmmoParticleDef
                    {
                        Ammo = new ParticleDef
                        {
                            Name = "", //Particle that surrounds your projectile. ShipWelderArc
                            Color = Color(red: 1f, green: 1f, blue: 1f, alpha: 1),
                            Offset = Vector(x: 0, y: 0, z: 0),
                            Extras = new ParticleOptionDef
                            {
                                Loop = true,
                                Restart = false,
                                MaxDistance = 5000,
                                MaxDuration = 1,
                                Scale = 0.25f,
                            },
                        },
                        Hit = new ParticleDef
                        {
                            Name = "",
                            ApplyToShield = true,
                            ShrinkByDistance = true,
                            Color = Color(red: 3, green: 2, blue: 1, alpha: 1),
                            Offset = Vector(x: 0, y: 0, z: 0),
                            Extras = new ParticleOptionDef
                            {
                                Loop = false,
                                Restart = false,
                                MaxDistance = 500,
                                MaxDuration = 1,
                                Scale = .25f,
                                HitPlayChance = 1f,
                            },
                        },
                    },
                    Lines = new LineDef
                    {
                        TracerMaterial = "ProjectileTrailLine", // Particle that follows your projectile. WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                        ColorVariance = Random(start: 5f, end: 10f), // multiply the color by random values within range.
                        WidthVariance = Random(start: 0f, end: 0.045f), // adds random value to default width (negatives shrinks width)
                        Tracer = new TracerBaseDef
                        {
                            Enable = true,
                            Length = .001f,
                            Width = .001f,
                            Color = Color(red: 0.2f, green: 0.9f, blue: 0.9f, alpha: 1),
                        },
                        Trail = new TrailDef
                        {
                            Enable = true,
                            Material = "WeaponLaser",
                            DecayTime = 10,
                            Color = Color(red: 57.5f, green: 45f, blue: 25f, alpha: 1),
                            Back = true,
                            CustomWidth = .2f,
                            UseWidthVariance = false,
                            UseColorFade = true,
                        },
                        OffsetEffect = new OffsetEffectDef
                        {
                            MaxOffset =  0,// 0 offset value disables this effect
                            MinLength = 0.2f,
                            MaxLength = 3,
                        },
                    },
                },
                AmmoAudio = new AmmoAudioDef
                {
                    TravelSound = "",
                    HitSound = "",
                    HitPlayChance = 0.1f,
                    HitPlayShield = true,
                }, // Don't edit below this line

        };
		
		private AmmoDef CrusaderMissile => new AmmoDef
        {
				AmmoMagazine = "CrusaderMissile", //Name of your ammo type definied in your ammo.sbc file.
                AmmoRound = "CrusaderMissile", //Desired model for the projectiles, must be defined in your ammo.sbc file.
                HybridRound = false, //AmmoMagazine based weapon with energy cost
                EnergyCost = 0.00000000001f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
                BaseDamage = 10000f, //Think of this as level of penetration, low base damage for no penetration. Every Block destroyed takes their HP off the projectile.
                Mass = 100f, // in kilograms, determines the amount of kinetic energy applied to the grid being hit.
                Health = 1, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying. KEEP THIS LOW. 1=Standard,10=Insane
                BackKickForce = 1000f, //Determines the amount of kinetic energy applied to the grid firing.
				DecayPerShot = 0f,
				HardPointUsable = false, // set to false if this is a Fragment ammoType and you don't want the turret to be able to select it directly.
				EnergyMagazineSize = 12, //ONLY USE IF "AmmoMagazine=ENERGY". Otherwise specify magazine size in Ammo.sbc file.
				IgnoreWater = false,

                Shape = new ShapeDef //defines the collision shape of projectile, defaults line and visual Line Length if set to 0
                {
                    Shape = LineShape,
                    Diameter = 1,
                },
                ObjectsHit = new ObjectsHitDef
                {
                    MaxObjectsHit = 0, // 0 = disabled
                    CountBlocks = false, // counts gridBlocks and not just entities hit
                },
                Fragment = new FragmentDef
				{
					AmmoRound = "HEShrapnel",
					Fragments = 10, //Number of Fragment projectiles created on impact.
					Degrees = 180,  //Think of this as a cone in front of the projectile, 360 degrees for a full circle.
					Reverse = false, //Redirects the cone back towards the projectile's rear
					RandomizeDir = false,
				},
                DamageScales = new DamageScaleDef
                {
                    MaxIntegrity = 0f, // 0 = disabled, 1000 = any blocks with currently integrity above 1000 will be immune to damage.
                    DamageVoxels = true, // true = voxels are vulnerable to this weapon
                    SelfDamage = false, // true = allow self damage.
                    HealthHitModifier = 3, // defaults to a value of 1, this setting modifies how much Health is subtracted from a projectile per hit (1 = per hit).
                    VoxelHitModifier = 3,  // modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01 = 1% damage, 2 = 200% damage.
                    Characters = 0.2f, //Damage to characters. 1f=same as damage to grids
                    Grids = new GridSizeDef
                    {
                        Large = 1f,
                        Small = 1f,
                    },
                    Armor = new ArmorDef
                    {
                        Armor = 1f,
                        Light = 1f,
                        Heavy = 11f,
                        NonArmor = 1f,
                    },
                    Shields = new ShieldDef
                    {
                        Modifier = .75f,
                        Type = Default,
                        BypassModifier = -1f,
                    },
                    // first true/false (ignoreOthers) will cause projectiles to pass through all blocks that do not match the custom subtypeIds.
                    Custom = new CustomScalesDef
                    {
                        IgnoreAllOthers = false,
                        Types = new []
                        {
                            new CustomBlocksDef
                            {
                                SubTypeId = "Test1",
                                Modifier = -1f,
                            },
                            new CustomBlocksDef
                            {
                                SubTypeId = "Test2",
                                Modifier = -1f,
                            },
                        },
                    },
                },
                AreaEffect = new AreaDamageDef
                {
                    AreaEffect = Explosive, // Disabled = do not use area effect at all, Explosive, Radiant, AntiSmart, JumpNullField, JumpNullField, EnergySinkField, AnchorField, EmpField, OffenseField, NavField, DotField.
                    AreaEffectDamage = 8000f, // 0 = DO NOT USE FOR EXPLOSIVES, USE DETONATION SETTINGS INSTEAD
                    AreaEffectRadius = 25f, // DO NOT USE FOR EXPLOSIVES, USE DETONATION SETTINGS INSTEAD
                    Pulse = new PulseDef // interval measured in game ticks (60 == 1 second), pulseChance chance (0 - 100) that an entity in field will be hit
                    {
                        Interval = 30,
                        PulseChance = 0,
                    },
                    Explosions = new ExplosionDef
                    {
                        NoVisuals = false, //Set to false to use vanilla explosion particles, unless specified a custom particle
                        NoSound = false, ////Set to false to use vanilla explosion noises, unless specified a custom sound
                        Scale = 4f, //Scale of the particles.
                        CustomParticle = "",
                        CustomSound = "",
                    },
                    Detonation = new DetonateDef //USE THESE MODIFIERS FOR EXPLOSIONS
                    {
                        DetonateOnEnd = true,
                        ArmOnlyOnHit = false,
                        DetonationDamage = 3000, //1 Steelplate =100HP, 1 large light armor cube=2500HP. Has a natural falloff based on radius.
                        DetonationRadius = 25, //Measured in meters, how large the explosion should be.
                    },
                    EwarFields = new EwarFieldsDef
                    {
                        Duration = 0,
                        StackDuration = true,
                        Depletable = false,
                        MaxStacks = 10,
                        TriggerRange = 5f,
                    },
                },
                Beams = new BeamDef
                {
                    Enable = false,
                    VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
                    ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
                    RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
                    OneParticle = false, // Only spawn one particle hit per beam weapon.
                },
                Trajectory = new TrajectoryDef
				{
					Guidance = Smart, //ammo guidance type, options are None, Smart, Remote(not used yet), TravelTo(if tracking travels to target Position, if none tracking travels to max trajectory, good for flak), DetectFixed(mines), DetectSmart(mines), DetectTravelTo(mines)
					TargetLossDegree = 180f,
					TargetLossTime = 90, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
					MaxLifeTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
					AccelPerSec = 100,
					DesiredSpeed = 1000,
					MaxTrajectory = 6000f,
					FieldTime = 0, // 0 is disabled, a value causes the projectile to come to rest, spawn a field and remain for a time (Measured in game ticks, 60 = 1 second)
					GravityMultiplier = 0f, // Gravity influences the trajectory of the projectile.
					SpeedVariance = Random(start: 0, end: 0), // subtracts value from DesiredSpeed
					RangeVariance = Random(start: 0, end: 0), // subtracts value from MaxTrajectory
					Smarts = new SmartsDef
					{
						Inaccuracy = 2f, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
						Aggressiveness = 3f, // controls how responsive tracking is.
						MaxLateralThrust = 0.5, // controls how sharp the trajectile may turn
						TrackingDelay = 200, // Measured in Shape diameter units traveled.
						MaxChaseTime = 2400, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
						OverideTarget = false, // when set to true ammo picks its own target, does not use hardpoint's.
						MaxTargets = 1,
					},
					Mines = new MinesDef
					{
						DetectRadius = 200,
						DeCloakRadius = 100,
						FieldTime = 0,
						Cloak = true,
						Persist = false,
					},
				},
                AmmoGraphics = new GraphicDef
                {
                    ModelName = "\\Models\\Akiad\\Small\\CrusaderMissile.mwm", //USE DOUBLE LINES, ex: "\\Models\\Name\\Small\\MyGun.mwm"
                    VisualProbability = 1f, //Percentage chance for the model to spawn, "1f" for 100% chance.
                    ShieldHitDraw = true,
                    Particles = new AmmoParticleDef
                    {
                        Ammo = new ParticleDef
                        {
                            Name = "", //Particle that surrounds your projectile. ShipWelderArc
                            Color = Color(red: 1f, green: 1f, blue: 1f, alpha: 1),
                            Offset = Vector(x: 0, y: 0, z: 0),
                            Extras = new ParticleOptionDef
                            {
                                Loop = true,
                                Restart = false,
                                MaxDistance = 5000,
                                MaxDuration = 1,
                                Scale = 0.25f,
                            },
                        },
                        Hit = new ParticleDef
                        {
                            Name = "",
                            ApplyToShield = true,
                            ShrinkByDistance = true,
                            Color = Color(red: 3, green: 2, blue: 1, alpha: 1),
                            Offset = Vector(x: 0, y: 0, z: 0),
                            Extras = new ParticleOptionDef
                            {
                                Loop = false,
                                Restart = false,
                                MaxDistance = 500,
                                MaxDuration = 1,
                                Scale = .25f,
                                HitPlayChance = 1f,
                            },
                        },
                    },
                    Lines = new LineDef
                    {
                        TracerMaterial = "ProjectileTrailLine", // Particle that follows your projectile. WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                        ColorVariance = Random(start: 5f, end: 10f), // multiply the color by random values within range.
                        WidthVariance = Random(start: 0f, end: 0.045f), // adds random value to default width (negatives shrinks width)
                        Tracer = new TracerBaseDef
                        {
                            Enable = true,
                            Length = .001f,
                            Width = .001f,
                            Color = Color(red: 0.2f, green: 0.9f, blue: 0.9f, alpha: 1),
                        },
                        Trail = new TrailDef
                        {
                            Enable = true,
                            Material = "WeaponLaser",
                            DecayTime = 20,
                            Color = Color(red: 115f, green: 90f, blue: 50f, alpha: 1),
                            Back = true,
                            CustomWidth = .5f,
                            UseWidthVariance = false,
                            UseColorFade = true,
                        },
                        OffsetEffect = new OffsetEffectDef
                        {
                            MaxOffset =  0,// 0 offset value disables this effect
                            MinLength = 0.2f,
                            MaxLength = 3,
                        },
                    },
                },
                AmmoAudio = new AmmoAudioDef
                {
                    TravelSound = "",
                    HitSound = "",
                    HitPlayChance = 0.1f,
                    HitPlayShield = true,
                }, // Don't edit below this line

        };
		private AmmoDef ArbiterMissile => new AmmoDef
        {
				AmmoMagazine = "ArbiterMissile", //Name of your ammo type definied in your ammo.sbc file.
                AmmoRound = "ArbiterMissile", //Desired model for the projectiles, must be defined in your ammo.sbc file.
                HybridRound = false, //AmmoMagazine based weapon with energy cost
                EnergyCost = 0.00000000001f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
                BaseDamage = 100000f, //Think of this as level of penetration, low base damage for no penetration. Every Block destroyed takes their HP off the projectile.
                Mass = 1000f, // in kilograms, determines the amount of kinetic energy applied to the grid being hit.
                Health = 8, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying. KEEP THIS LOW. 1=Standard,10=Insane
                BackKickForce = 10000f, //Determines the amount of kinetic energy applied to the grid firing.
				DecayPerShot = 0f,
				HardPointUsable = true, // set to false if this is a Fragment ammoType and you don't want the turret to be able to select it directly.
				EnergyMagazineSize = 12, //ONLY USE IF "AmmoMagazine=ENERGY". Otherwise specify magazine size in Ammo.sbc file.
				IgnoreWater = false,

                Shape = new ShapeDef //defines the collision shape of projectile, defaults line and visual Line Length if set to 0
                {
                    Shape = LineShape,
                    Diameter = 1,
                },
                ObjectsHit = new ObjectsHitDef
                {
                    MaxObjectsHit = 0, // 0 = disabled
                    CountBlocks = false, // counts gridBlocks and not just entities hit
                },
                Fragment = new FragmentDef
				{
					AmmoRound = "HEShrapnel",
					Fragments = 10, //Number of Fragment projectiles created on impact.
					Degrees = 180,  //Think of this as a cone in front of the projectile, 360 degrees for a full circle.
					Reverse = false, //Redirects the cone back towards the projectile's rear
					RandomizeDir = false,
				},
				DamageScales = new DamageScaleDef
				{
					MaxIntegrity = 0f, // 0 = disabled, 1000 = any blocks with currently integrity above 1000 will be immune to damage.
					DamageVoxels = false, // true = voxels are vulnerable to this weapon
					SelfDamage = false, // true = allow self damage.
					HealthHitModifier = 120f, // defaults to a value of 1, this setting modifies how much Health is subtracted from a projectile per hit (1 = per hit).
					VoxelHitModifier = -1f,
					Characters = 1f,
					// modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01 = 1% damage, 2 = 200% damage.
					FallOff = new FallOffDef
					{
						Distance = 0f, // Distance at which max damage begins falling off.
						MinMultipler = 0f, // value from 0.0f to 1f where 0.1f would be a min damage of 10% of max damage.
					},
					Grids = new GridSizeDef
					{
						Large = 8f,
						Small = 10f,
					},
					Armor = new ArmorDef
					{
						Armor = 6f,
						Light = 5f,
						Heavy = 5f,
						NonArmor = 9f,
					},
					Shields = new ShieldDef
					{
						Modifier = .75f,
						Type = Default,
						BypassModifier = -1f,
					},
					// first true/false (ignoreOthers) will cause projectiles to pass through all blocks that do not match the custom subtypeIds.
					Custom = new CustomScalesDef
					{
						IgnoreAllOthers = false,
						Types = new[]
						{
							new CustomBlocksDef
							{
								SubTypeId = "Test1",
								Modifier = -1f,
							},
							new CustomBlocksDef
							{
								SubTypeId = "Test2",
								Modifier = -1f,
							},
						},
					},
				},
				AreaEffect = new AreaDamageDef
				{
					AreaEffect = Explosive, // Disabled = do not use area effect at all, Explosive, Radiant, AntiSmart, JumpNullField, JumpNullField, EnergySinkField, AnchorField, EmpField, OffenseField, NavField, DotField.
					Base = new AreaInfluence
					{
						Radius = 1f, // the sphere of influence of area effects
						EffectStrength = 1f, // For ewar it applies this amount per pulse/hit, non-ewar applies this as damage per tick per entity in area of influence. For radiant 0 == use spillover from BaseDamage, otherwise use this value.
					},
					Pulse = new PulseDef // interval measured in game ticks (60 == 1 second), pulseChance chance (0 - 100) that an entity in field will be hit
					{
						Interval = 0,
						PulseChance = 0,
						GrowTime = 0,
						HideModel = false,
						ShowParticle = false,
						Particle = new ParticleDef
						{
							Name = "", //ShipWelderArc
							ShrinkByDistance = false,
							Color = Color(red: 128, green: 0, blue: 0, alpha: 32),
							Offset = Vector(x: 0, y: -1, z: 0),
							Extras = new ParticleOptionDef
							{
								Loop = false,
								Restart = false,
								MaxDistance = 5000,
								MaxDuration = 1,
								Scale = 1,
							},
						},
					},
					Explosions = new ExplosionDef
					{
						NoVisuals = false,
						NoSound = false,
						NoShrapnel = false,
						NoDeformation = false,
						Scale = 3f,
						CustomParticle = "ShivaNuke",
						CustomSound = "ArcWepLrgWarheadExpl",
					},
					Detonation = new DetonateDef
					{
						DetonateOnEnd = true,
						ArmOnlyOnHit = true,
						DetonationDamage = 160000f,
						DetonationRadius = 400f,
						MinArmingTime = 0, //Min time in ticks before projectile will arm for detonation (will also affect Fragment spawning)
					},
                    EwarFields = new EwarFieldsDef
                    {
                        Duration = 0,
                        StackDuration = true,
                        Depletable = false,
                        MaxStacks = 10,
                        TriggerRange = 5f,
                    },
                },
                Beams = new BeamDef
                {
                    Enable = false,
                    VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
                    ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
                    RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
                    OneParticle = false, // Only spawn one particle hit per beam weapon.
                },
                Trajectory = new TrajectoryDef
				{
					Guidance = Smart, //ammo guidance type, options are None, Smart, Remote(not used yet), TravelTo(if tracking travels to target Position, if none tracking travels to max trajectory, good for flak), DetectFixed(mines), DetectSmart(mines), DetectTravelTo(mines)
					TargetLossDegree = 180f,
					TargetLossTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
					MaxLifeTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
					AccelPerSec = 300,
					DesiredSpeed = 1200,
					MaxTrajectory = 30000f,
					FieldTime = 0, // 0 is disabled, a value causes the projectile to come to rest, spawn a field and remain for a time (Measured in game ticks, 60 = 1 second)
					GravityMultiplier = 0f, // Gravity influences the trajectory of the projectile.
					SpeedVariance = Random(start: 0, end: 0), // subtracts value from DesiredSpeed
					RangeVariance = Random(start: 0, end: 0), // subtracts value from MaxTrajectory
					Smarts = new SmartsDef
					{
						Inaccuracy = 10f, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
						Aggressiveness = 3f, // controls how responsive tracking is.
						MaxLateralThrust = 0.5, // controls how sharp the trajectile may turn
						TrackingDelay = 500, // Measured in Shape diameter units traveled.
						MaxChaseTime = 2400, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
						OverideTarget = false, // when set to true ammo picks its own target, does not use hardpoint's.
						MaxTargets = 1,
					},
					Mines = new MinesDef
					{
						DetectRadius = 200,
						DeCloakRadius = 100,
						FieldTime = 0,
						Cloak = true,
						Persist = false,
					},
				},
                AmmoGraphics = new GraphicDef
                {
                    ModelName = "\\Models\\Akiad\\Small\\ArbiterMissile.mwm", //USE DOUBLE LINES, ex: "\\Models\\Name\\Small\\MyGun.mwm"
                    VisualProbability = 1f, //Percentage chance for the model to spawn, "1f" for 100% chance.
                    ShieldHitDraw = true,
                    Particles = new AmmoParticleDef
                    {
                        Ammo = new ParticleDef
                        {
                            Name = "MyMissile", //Particle that surrounds your projectile. ShipWelderArc
                            Color = Color(red: 1f, green: 1f, blue: 1f, alpha: 1),
                            Offset = Vector(x: 0, y: 0, z: 20),
                            Extras = new ParticleOptionDef
                            {
                                Loop = true,
                                Restart = false,
                                MaxDistance = 5000,
                                MaxDuration = 1,
                                Scale = 5f,
                            },
                        },
                        Hit = new ParticleDef
                        {
                            Name = "",
                            ApplyToShield = true,
                            ShrinkByDistance = true,
                            Color = Color(red: 3, green: 2, blue: 1, alpha: 1),
                            Offset = Vector(x: 0, y: 0, z: 0),
                            Extras = new ParticleOptionDef
                            {
                                Loop = false,
                                Restart = false,
                                MaxDistance = 500,
                                MaxDuration = 1,
                                Scale = .25f,
                                HitPlayChance = 1f,
                            },
                        },
                    },
                    Lines = new LineDef
                    {
                        TracerMaterial = "ProjectileTrailLine", // Particle that follows your projectile. WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                        ColorVariance = Random(start: 5f, end: 10f), // multiply the color by random values within range.
                        WidthVariance = Random(start: 0f, end: 0.045f), // adds random value to default width (negatives shrinks width)
                        Tracer = new TracerBaseDef
                        {
                            Enable = true,
                            Length = .001f,
                            Width = .001f,
                            Color = Color(red: 0.2f, green: 0.9f, blue: 0.9f, alpha: 1),
                        },
                        Trail = new TrailDef
                        {
                            Enable = true,
                            Material = "WeaponLaser",
                            DecayTime = 20,
                            Color = Color(red: 115f, green: 90f, blue: 50f, alpha: 1),
                            Back = true,
                            CustomWidth = .5f,
                            UseWidthVariance = false,
                            UseColorFade = true,
                        },
                        OffsetEffect = new OffsetEffectDef
                        {
                            MaxOffset =  0,// 0 offset value disables this effect
                            MinLength = 0.2f,
                            MaxLength = 3,
                        },
                    },
                },
                AmmoAudio = new AmmoAudioDef
                {
                    TravelSound = "",
                    HitSound = "",
                    HitPlayChance = 0.1f,
                    HitPlayShield = true,
                }, // Don't edit below this line

        };
		
		private AmmoDef LotusMissile1 => new AmmoDef
        {
				AmmoMagazine = "LotusMissile", //Name of your ammo type definied in your ammo.sbc file.
                AmmoRound = "AMS", //Desired model for the projectiles, must be defined in your ammo.sbc file.
                HybridRound = false, //AmmoMagazine based weapon with energy cost
                EnergyCost = 0.00000000001f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
                BaseDamage = 0f, //Think of this as level of penetration, low base damage for no penetration. Every Block destroyed takes their HP off the projectile.
                Mass = 100f, // in kilograms, determines the amount of kinetic energy applied to the grid being hit.
                Health = 1, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying. KEEP THIS LOW. 1=Standard,10=Insane
                BackKickForce = 1000f, //Determines the amount of kinetic energy applied to the grid firing.
				DecayPerShot = 0f,
				HardPointUsable = true, // set to false if this is a Fragment ammoType and you don't want the turret to be able to select it directly.
				EnergyMagazineSize = 12, //ONLY USE IF "AmmoMagazine=ENERGY". Otherwise specify magazine size in Ammo.sbc file.
				IgnoreWater = false,

                Shape = new ShapeDef //defines the collision shape of projectile, defaults line and visual Line Length if set to 0
                {
                    Shape = LineShape,
                    Diameter = 1,
                },
                ObjectsHit = new ObjectsHitDef
                {
                    MaxObjectsHit = 0, // 0 = disabled
                    CountBlocks = false, // counts gridBlocks and not just entities hit
                },
                Fragment = new FragmentDef
				{
					AmmoRound = "LotusMine1",
					Fragments = 2, //Number of Fragment projectiles created on impact.
					Degrees = 180,  //Think of this as a cone in front of the projectile, 360 degrees for a full circle.
					Reverse = false, //Redirects the cone back towards the projectile's rear
					RandomizeDir = false,
				},
                DamageScales = new DamageScaleDef
                {
                    MaxIntegrity = 0f, // 0 = disabled, 1000 = any blocks with currently integrity above 1000 will be immune to damage.
                    DamageVoxels = false, // true = voxels are vulnerable to this weapon
                    SelfDamage = false, // true = allow self damage.
                    HealthHitModifier = 1, // defaults to a value of 1, this setting modifies how much Health is subtracted from a projectile per hit (1 = per hit).
                    VoxelHitModifier = 3,  // modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01 = 1% damage, 2 = 200% damage.
                    Characters = 0.2f, //Damage to characters. 1f=same as damage to grids
                    Grids = new GridSizeDef
                    {
                        Large = -1f,
                        Small = -1f,
                    },
                    Armor = new ArmorDef
                    {
                        Armor = -1f,
                        Light = -1f,
                        Heavy = -1f,
                        NonArmor = 1f,
                    },
                    Shields = new ShieldDef
                    {
                        Modifier = .75f,
                        Type = Default,
                        BypassModifier = -1f,
                    },
                    // first true/false (ignoreOthers) will cause projectiles to pass through all blocks that do not match the custom subtypeIds.
                    Custom = new CustomScalesDef
                    {
                        IgnoreAllOthers = false,
                        Types = new []
                        {
                            new CustomBlocksDef
                            {
                                SubTypeId = "Test1",
                                Modifier = -1f,
                            },
                            new CustomBlocksDef
                            {
                                SubTypeId = "Test2",
                                Modifier = -1f,
                            },
                        },
                    },
                },
                AreaEffect = new AreaDamageDef
                {
                    AreaEffect = Disabled, // Disabled = do not use area effect at all, Explosive, Radiant, AntiSmart, JumpNullField, JumpNullField, EnergySinkField, AnchorField, EmpField, OffenseField, NavField, DotField.
                    AreaEffectDamage = 8000f, // 0 = DO NOT USE FOR EXPLOSIVES, USE DETONATION SETTINGS INSTEAD
                    AreaEffectRadius = 25f, // DO NOT USE FOR EXPLOSIVES, USE DETONATION SETTINGS INSTEAD
                    Pulse = new PulseDef // interval measured in game ticks (60 == 1 second), pulseChance chance (0 - 100) that an entity in field will be hit
                    {
                        Interval = 30,
                        PulseChance = 0,
                    },
                    Explosions = new ExplosionDef
                    {
                        NoVisuals = false, //Set to false to use vanilla explosion particles, unless specified a custom particle
                        NoSound = false, ////Set to false to use vanilla explosion noises, unless specified a custom sound
                        Scale = 4f, //Scale of the particles.
                        CustomParticle = "",
                        CustomSound = "",
                    },
                    Detonation = new DetonateDef //USE THESE MODIFIERS FOR EXPLOSIONS
                    {
                        DetonateOnEnd = false,
                        ArmOnlyOnHit = false,
                        DetonationDamage = 3000, //1 Steelplate =100HP, 1 large light armor cube=2500HP. Has a natural falloff based on radius.
                        DetonationRadius = 25, //Measured in meters, how large the explosion should be.
                    },
                    EwarFields = new EwarFieldsDef
                    {
                        Duration = 0,
                        StackDuration = true,
                        Depletable = false,
                        MaxStacks = 10,
                        TriggerRange = 5f,
                    },
                },
                Beams = new BeamDef
                {
                    Enable = false,
                    VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
                    ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
                    RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
                    OneParticle = false, // Only spawn one particle hit per beam weapon.
                },
                Trajectory = new TrajectoryDef
				{
					Guidance = None, //ammo guidance type, options are None, Smart, Remote(not used yet), TravelTo(if tracking travels to target Position, if none tracking travels to max trajectory, good for flak), DetectFixed(mines), DetectSmart(mines), DetectTravelTo(mines)
					TargetLossDegree = 180f,
					TargetLossTime = 90, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
					MaxLifeTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
					AccelPerSec = 100,
					DesiredSpeed = 1000,
					MaxTrajectory = 200f,
					FieldTime = 0, // 0 is disabled, a value causes the projectile to come to rest, spawn a field and remain for a time (Measured in game ticks, 60 = 1 second)
					GravityMultiplier = 0f, // Gravity influences the trajectory of the projectile.
					SpeedVariance = Random(start: 0, end: 0), // subtracts value from DesiredSpeed
					RangeVariance = Random(start: 0, end: 0), // subtracts value from MaxTrajectory
					Smarts = new SmartsDef
					{
						Inaccuracy = 2f, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
						Aggressiveness = 3f, // controls how responsive tracking is.
						MaxLateralThrust = 0.5, // controls how sharp the trajectile may turn
						TrackingDelay = 200, // Measured in Shape diameter units traveled.
						MaxChaseTime = 2400, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
						OverideTarget = false, // when set to true ammo picks its own target, does not use hardpoint's.
						MaxTargets = 1,
					},
					Mines = new MinesDef
					{
						DetectRadius = 200,
						DeCloakRadius = 100,
						FieldTime = 0,
						Cloak = true,
						Persist = false,
					},
				},
                AmmoGraphics = new GraphicDef
                {
                    ModelName = "\\Models\\Akiad\\Small\\LotusMissile.mwm", //USE DOUBLE LINES, ex: "\\Models\\Name\\Small\\MyGun.mwm"
                    VisualProbability = 1f, //Percentage chance for the model to spawn, "1f" for 100% chance.
                    ShieldHitDraw = true,
                    Particles = new AmmoParticleDef
                    {
                        Ammo = new ParticleDef
                        {
                            Name = "", //Particle that surrounds your projectile. ShipWelderArc
                            Color = Color(red: 1f, green: 1f, blue: 1f, alpha: 1),
                            Offset = Vector(x: 0, y: 0, z: 0),
                            Extras = new ParticleOptionDef
                            {
                                Loop = true,
                                Restart = false,
                                MaxDistance = 5000,
                                MaxDuration = 1,
                                Scale = 0.25f,
                            },
                        },
                        Hit = new ParticleDef
                        {
                            Name = "",
                            ApplyToShield = true,
                            ShrinkByDistance = true,
                            Color = Color(red: 3, green: 2, blue: 1, alpha: 1),
                            Offset = Vector(x: 0, y: 0, z: 0),
                            Extras = new ParticleOptionDef
                            {
                                Loop = false,
                                Restart = false,
                                MaxDistance = 500,
                                MaxDuration = 1,
                                Scale = .25f,
                                HitPlayChance = 1f,
                            },
                        },
                    },
                    Lines = new LineDef
                    {
                        TracerMaterial = "ProjectileTrailLine", // Particle that follows your projectile. WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                        ColorVariance = Random(start: 5f, end: 10f), // multiply the color by random values within range.
                        WidthVariance = Random(start: 0f, end: 0.045f), // adds random value to default width (negatives shrinks width)
                        Tracer = new TracerBaseDef
                        {
                            Enable = true,
                            Length = .001f,
                            Width = .001f,
                            Color = Color(red: 0.2f, green: 0.9f, blue: 0.9f, alpha: 1),
                        },
                        Trail = new TrailDef
                        {
                            Enable = true,
                            Material = "WeaponLaser",
                            DecayTime = 2,
                            Color = Color(red: 115f, green: 90f, blue: 50f, alpha: 1),
                            Back = true,
                            CustomWidth = .1f,
                            UseWidthVariance = false,
                            UseColorFade = true,
                        },
                        OffsetEffect = new OffsetEffectDef
                        {
                            MaxOffset =  0,// 0 offset value disables this effect
                            MinLength = 0.2f,
                            MaxLength = 3,
                        },
                    },
                },
                AmmoAudio = new AmmoAudioDef
                {
                    TravelSound = "",
                    HitSound = "",
                    HitPlayChance = 0.1f,
                    HitPlayShield = true,
                }, // Don't edit below this line

        };
		private AmmoDef LotusMine1 => new AmmoDef
        {
				AmmoMagazine = "LotusMine", //Name of your ammo type definied in your ammo.sbc file.
                AmmoRound = "LotusMine1", //Desired model for the projectiles, must be defined in your ammo.sbc file.
                HybridRound = false, //AmmoMagazine based weapon with energy cost
                EnergyCost = 0.00000000001f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
                BaseDamage = 20f, //Think of this as level of penetration, low base damage for no penetration. Every Block destroyed takes their HP off the projectile.
                Mass = 100f, // in kilograms, determines the amount of kinetic energy applied to the grid being hit.
                Health = 1, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying. KEEP THIS LOW. 1=Standard,10=Insane
                BackKickForce = 1000f, //Determines the amount of kinetic energy applied to the grid firing.
				DecayPerShot = 0f,
				HardPointUsable = false, // set to false if this is a Fragment ammoType and you don't want the turret to be able to select it directly.
				EnergyMagazineSize = 12, //ONLY USE IF "AmmoMagazine=ENERGY". Otherwise specify magazine size in Ammo.sbc file.
				IgnoreWater = false,

                Shape = new ShapeDef //defines the collision shape of projectile, defaults line and visual Line Length if set to 0
                {
                    Shape = LineShape,
                    Diameter = 1,
                },
                ObjectsHit = new ObjectsHitDef
                {
                    MaxObjectsHit = 0, // 0 = disabled
                    CountBlocks = false, // counts gridBlocks and not just entities hit
                },
                Fragment = new FragmentDef
				{
					AmmoRound = "",
					Fragments = 20, //Number of Fragment projectiles created on impact.
					Degrees = 360,  //Think of this as a cone in front of the projectile, 360 degrees for a full circle.
					Reverse = false, //Redirects the cone back towards the projectile's rear
					RandomizeDir = false,
				},
                DamageScales = new DamageScaleDef
                {
                    MaxIntegrity = 0f, // 0 = disabled, 1000 = any blocks with currently integrity above 1000 will be immune to damage.
                    DamageVoxels = false, // true = voxels are vulnerable to this weapon
                    SelfDamage = false, // true = allow self damage.
                    HealthHitModifier = 1, // defaults to a value of 1, this setting modifies how much Health is subtracted from a projectile per hit (1 = per hit).
                    VoxelHitModifier = 3,  // modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01 = 1% damage, 2 = 200% damage.
                    Characters = 0.2f, //Damage to characters. 1f=same as damage to grids
                    Grids = new GridSizeDef
                    {
                        Large = -1f,
                        Small = -1f,
                    },
                    Armor = new ArmorDef
                    {
                        Armor = -1f,
                        Light = -1f,
                        Heavy = -1f,
                        NonArmor = 1f,
                    },
                    Shields = new ShieldDef
                    {
                        Modifier = .75f,
                        Type = Default,
                        BypassModifier = -1f,
                    },
                    // first true/false (ignoreOthers) will cause projectiles to pass through all blocks that do not match the custom subtypeIds.
                    Custom = new CustomScalesDef
                    {
                        IgnoreAllOthers = false,
                        Types = new []
                        {
                            new CustomBlocksDef
                            {
                                SubTypeId = "Test1",
                                Modifier = -1f,
                            },
                            new CustomBlocksDef
                            {
                                SubTypeId = "Test2",
                                Modifier = -1f,
                            },
                        },
                    },
                },
					AreaEffect = new AreaDamageDef
					{
						AreaEffect = AntiSmart, // Disabled = do not use area effect at all, Explosive, Radiant, AntiSmart, JumpNullField, JumpNullField, EnergySinkField, AnchorField, EmpField, OffenseField, NavField, DotField.
						Base = new AreaInfluence
						{
							Radius = 50f, // the sphere of influence of area effects
							EffectStrength = 100f, // For ewar it applies this amount per pulse/hit, non-ewar applies this as damage per tick per entity in area of influence. For radiant 0 == use spillover from BaseDamage, otherwise use this value.
						},
						Pulse = new PulseDef // interval measured in game ticks (60 == 1 second), pulseChance chance (0 - 100) that an entity in field will be hit
						{
							Interval = 0,
							PulseChance = 100,
							GrowTime = 1,
							HideModel = false,
							ShowParticle = true,
							Particle = new ParticleDef
							{
								Name = "", //ShipWelderArc
								ShrinkByDistance = false,
								Color = Color(red: 128, green: 0, blue: 0, alpha: 32),
								Offset = Vector(x: 0, y: -1, z: 0),
								Extras = new ParticleOptionDef
								{
									Loop = false,
									Restart = false,
									MaxDistance = 5000,
									MaxDuration = 1,
									Scale = 1,
								},
							},
						},
						Explosions = new ExplosionDef
						{
							NoVisuals = false,
							NoSound = false,
							NoShrapnel = false,
							NoDeformation = false,
							Scale = 3,
							CustomParticle = "",
							CustomSound = "ArcWepLrgWarheadExpl",
						},
						Detonation = new DetonateDef
						{
							DetonateOnEnd = true,
							ArmOnlyOnHit = false,
							DetonationDamage = 7500f,
							DetonationRadius = 25f,
							MinArmingTime = 0, //Min time in ticks before projectile will arm for detonation (will also affect Fragment spawning)
						},
						EwarFields = new EwarFieldsDef
						{
							Duration = 6000,
							StackDuration = false,
							Depletable = false,
							MaxStacks = 1,
							TriggerRange = 50f,
							DisableParticleEffect = false,
							Force = new PushPullDef // AreaEffectDamage is multiplied by target mass.
							{
								ForceFrom = ProjectileLastPosition, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
								ForceTo = HitPosition, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
								Position = TargetCenterOfMass, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
							},
						},
					},
					Beams = new BeamDef
					{
						Enable = false,
						VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
						ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
						RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
						OneParticle = false, // Only spawn one particle hit per beam weapon.
					},
					Trajectory = new TrajectoryDef
					{
						Guidance = DetectFixed,
						TargetLossDegree = 80f,
						TargetLossTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
						MaxLifeTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
						AccelPerSec = 1f,
						DesiredSpeed = 0, // DO NOT SET HIGHER THAN 4100
						MaxTrajectory = 0f,
						FieldTime = 6000, // 0 is disabled, a value causes the projectile to come to rest, spawn a field and remain for a time (Measured in game ticks, 60 = 1 second)
						GravityMultiplier = 0f, // Gravity multiplier, influences the trajectory of the projectile, value greater than 0 to enable.
						SpeedVariance = Random(start: 0, end: 0), // subtracts value from DesiredSpeed
						RangeVariance = Random(start: 0, end: 0), // subtracts value from MaxTrajectory
						MaxTrajectoryTime = 0, // How long the weapon must fire before it reaches MaxTrajectory.
						Smarts = new SmartsDef
						{
							Inaccuracy = 0f, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
							Aggressiveness = 1f, // controls how responsive tracking is.
							MaxLateralThrust = 0.5, // controls how sharp the trajectile may turn
							TrackingDelay = 0, // Measured in Shape diameter units traveled.
							MaxChaseTime = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
							OverideTarget = false, // when set to true ammo picks its own target, does not use hardpoint's.
							MaxTargets = 0, // Number of targets allowed before ending, 0 = unlimited
							NoTargetExpire = false, // Expire without ever having a target at TargetLossTime
							Roam = false, // Roam current area after target loss
							KeepAliveAfterTargetLoss = true, // Whether to stop early death of projectile on target loss
						},
						Mines = new MinesDef
						{
							DetectRadius = 25f,
							DeCloakRadius = 0f,
							FieldTime = 6000,
							Cloak = false,
							Persist = true,
						},
					},
                AmmoGraphics = new GraphicDef
                {
                    ModelName = "\\Models\\Akiad\\Small\\LotusMine.mwm", //USE DOUBLE LINES, ex: "\\Models\\Name\\Small\\MyGun.mwm"
                    VisualProbability = 1f, //Percentage chance for the model to spawn, "1f" for 100% chance.
                    ShieldHitDraw = true,
                    Particles = new AmmoParticleDef
                    {
                        Ammo = new ParticleDef
                        {
                            Name = "Lotus2", //Particle that surrounds your projectile. ShipWelderArc
                            Color = Color(red: 1f, green: 1f, blue: 1f, alpha: 1),
                            Offset = Vector(x: 0, y: 0, z: 0),
                            Extras = new ParticleOptionDef
                            {
                                Loop = true,
                                Restart = false,
                                MaxDistance = 5000,
                                MaxDuration = 1,
                                Scale = 0.25f,
                            },
                        },
                        Hit = new ParticleDef
                        {
                            Name = "",
                            ApplyToShield = true,
                            ShrinkByDistance = true,
                            Color = Color(red: 3, green: 2, blue: 1, alpha: 1),
                            Offset = Vector(x: 0, y: 0, z: 0),
                            Extras = new ParticleOptionDef
                            {
                                Loop = false,
                                Restart = false,
                                MaxDistance = 500,
                                MaxDuration = 1,
                                Scale = .25f,
                                HitPlayChance = 1f,
                            },
                        },
                    },
                    Lines = new LineDef
                    {
                        TracerMaterial = "ProjectileTrailLine", // Particle that follows your projectile. WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                        ColorVariance = Random(start: 5f, end: 10f), // multiply the color by random values within range.
                        WidthVariance = Random(start: 0f, end: 0.045f), // adds random value to default width (negatives shrinks width)
                        Tracer = new TracerBaseDef
                        {
                            Enable = true,
                            Length = .001f,
                            Width = .001f,
                            Color = Color(red: 0.2f, green: 0.9f, blue: 0.9f, alpha: 1),
                        },
                        Trail = new TrailDef
                        {
                            Enable = true,
                            Material = "WeaponLaser",
                            DecayTime = 2,
                            Color = Color(red: 115f, green: 90f, blue: 50f, alpha: 1),
                            Back = true,
                            CustomWidth = .1f,
                            UseWidthVariance = false,
                            UseColorFade = true,
                        },
                        OffsetEffect = new OffsetEffectDef
                        {
                            MaxOffset =  0,// 0 offset value disables this effect
                            MinLength = 0.2f,
                            MaxLength = 3,
                        },
                    },
                },
                AmmoAudio = new AmmoAudioDef
                {
                    TravelSound = "",
                    HitSound = "",
                    HitPlayChance = 0.1f,
                    HitPlayShield = true,
                }, // Don't edit below this line

        };
		private AmmoDef LotusMissile2 => new AmmoDef
        {
				AmmoMagazine = "LotusMissile", //Name of your ammo type definied in your ammo.sbc file.
                AmmoRound = "Repel", //Desired model for the projectiles, must be defined in your ammo.sbc file.
                HybridRound = false, //AmmoMagazine based weapon with energy cost
                EnergyCost = 0.00000000001f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
                BaseDamage = 0f, //Think of this as level of penetration, low base damage for no penetration. Every Block destroyed takes their HP off the projectile.
                Mass = 100f, // in kilograms, determines the amount of kinetic energy applied to the grid being hit.
                Health = 1, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying. KEEP THIS LOW. 1=Standard,10=Insane
                BackKickForce = 1000f, //Determines the amount of kinetic energy applied to the grid firing.
				DecayPerShot = 0f,
				HardPointUsable = true, // set to false if this is a Fragment ammoType and you don't want the turret to be able to select it directly.
				EnergyMagazineSize = 12, //ONLY USE IF "AmmoMagazine=ENERGY". Otherwise specify magazine size in Ammo.sbc file.
				IgnoreWater = false,

                Shape = new ShapeDef //defines the collision shape of projectile, defaults line and visual Line Length if set to 0
                {
                    Shape = LineShape,
                    Diameter = 1,
                },
                ObjectsHit = new ObjectsHitDef
                {
                    MaxObjectsHit = 0, // 0 = disabled
                    CountBlocks = false, // counts gridBlocks and not just entities hit
                },
                Fragment = new FragmentDef
				{
					AmmoRound = "LotusMineRepel",
					Fragments = 2, //Number of Fragment projectiles created on impact.
					Degrees = 180,  //Think of this as a cone in front of the projectile, 360 degrees for a full circle.
					Reverse = false, //Redirects the cone back towards the projectile's rear
					RandomizeDir = false,
				},
                DamageScales = new DamageScaleDef
                {
                    MaxIntegrity = 0f, // 0 = disabled, 1000 = any blocks with currently integrity above 1000 will be immune to damage.
                    DamageVoxels = false, // true = voxels are vulnerable to this weapon
                    SelfDamage = false, // true = allow self damage.
                    HealthHitModifier = 1, // defaults to a value of 1, this setting modifies how much Health is subtracted from a projectile per hit (1 = per hit).
                    VoxelHitModifier = 3,  // modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01 = 1% damage, 2 = 200% damage.
                    Characters = 0.2f, //Damage to characters. 1f=same as damage to grids
                    Grids = new GridSizeDef
                    {
                        Large = -1f,
                        Small = -1f,
                    },
                    Armor = new ArmorDef
                    {
                        Armor = -1f,
                        Light = -1f,
                        Heavy = -1f,
                        NonArmor = 1f,
                    },
                    Shields = new ShieldDef
                    {
                        Modifier = .75f,
                        Type = Default,
                        BypassModifier = -1f,
                    },
                    // first true/false (ignoreOthers) will cause projectiles to pass through all blocks that do not match the custom subtypeIds.
                    Custom = new CustomScalesDef
                    {
                        IgnoreAllOthers = false,
                        Types = new []
                        {
                            new CustomBlocksDef
                            {
                                SubTypeId = "Test1",
                                Modifier = -1f,
                            },
                            new CustomBlocksDef
                            {
                                SubTypeId = "Test2",
                                Modifier = -1f,
                            },
                        },
                    },
                },
                AreaEffect = new AreaDamageDef
                {
                    AreaEffect = Disabled, // Disabled = do not use area effect at all, Explosive, Radiant, AntiSmart, JumpNullField, JumpNullField, EnergySinkField, AnchorField, EmpField, OffenseField, NavField, DotField.
                    AreaEffectDamage = 1000f, // 0 = DO NOT USE FOR EXPLOSIVES, USE DETONATION SETTINGS INSTEAD
                    AreaEffectRadius = 25f, // DO NOT USE FOR EXPLOSIVES, USE DETONATION SETTINGS INSTEAD
                    Pulse = new PulseDef // interval measured in game ticks (60 == 1 second), pulseChance chance (0 - 100) that an entity in field will be hit
                    {
                        Interval = 30,
                        PulseChance = 0,
                    },
                    Explosions = new ExplosionDef
                    {
                        NoVisuals = false, //Set to false to use vanilla explosion particles, unless specified a custom particle
                        NoSound = false, ////Set to false to use vanilla explosion noises, unless specified a custom sound
                        Scale = 4f, //Scale of the particles.
                        CustomParticle = "",
                        CustomSound = "",
                    },
                    Detonation = new DetonateDef //USE THESE MODIFIERS FOR EXPLOSIONS
                    {
                        DetonateOnEnd = false,
                        ArmOnlyOnHit = false,
                        DetonationDamage = 3000, //1 Steelplate =100HP, 1 large light armor cube=2500HP. Has a natural falloff based on radius.
                        DetonationRadius = 25, //Measured in meters, how large the explosion should be.
                    },
                    EwarFields = new EwarFieldsDef
                    {
                        Duration = 0,
                        StackDuration = true,
                        Depletable = false,
                        MaxStacks = 10,
                        TriggerRange = 5f,
                    },
                },
                Beams = new BeamDef
                {
                    Enable = false,
                    VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
                    ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
                    RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
                    OneParticle = false, // Only spawn one particle hit per beam weapon.
                },
                Trajectory = new TrajectoryDef
				{
					Guidance = None, //ammo guidance type, options are None, Smart, Remote(not used yet), TravelTo(if tracking travels to target Position, if none tracking travels to max trajectory, good for flak), DetectFixed(mines), DetectSmart(mines), DetectTravelTo(mines)
					TargetLossDegree = 180f,
					TargetLossTime = 90, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
					MaxLifeTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
					AccelPerSec = 100,
					DesiredSpeed = 1000,
					MaxTrajectory = 200f,
					FieldTime = 0, // 0 is disabled, a value causes the projectile to come to rest, spawn a field and remain for a time (Measured in game ticks, 60 = 1 second)
					GravityMultiplier = 0f, // Gravity influences the trajectory of the projectile.
					SpeedVariance = Random(start: 0, end: 0), // subtracts value from DesiredSpeed
					RangeVariance = Random(start: 0, end: 0), // subtracts value from MaxTrajectory
					Smarts = new SmartsDef
					{
						Inaccuracy = 2f, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
						Aggressiveness = 3f, // controls how responsive tracking is.
						MaxLateralThrust = 0.5, // controls how sharp the trajectile may turn
						TrackingDelay = 200, // Measured in Shape diameter units traveled.
						MaxChaseTime = 2400, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
						OverideTarget = false, // when set to true ammo picks its own target, does not use hardpoint's.
						MaxTargets = 1,
					},
					Mines = new MinesDef
					{
						DetectRadius = 200,
						DeCloakRadius = 100,
						FieldTime = 0,
						Cloak = true,
						Persist = false,
					},
				},
                AmmoGraphics = new GraphicDef
                {
                    ModelName = "\\Models\\Akiad\\Small\\LotusMissile.mwm", //USE DOUBLE LINES, ex: "\\Models\\Name\\Small\\MyGun.mwm"
                    VisualProbability = 1f, //Percentage chance for the model to spawn, "1f" for 100% chance.
                    ShieldHitDraw = true,
                    Particles = new AmmoParticleDef
                    {
                        Ammo = new ParticleDef
                        {
                            Name = "", //Particle that surrounds your projectile. ShipWelderArc
                            Color = Color(red: 1f, green: 1f, blue: 1f, alpha: 1),
                            Offset = Vector(x: 0, y: 0, z: 0),
                            Extras = new ParticleOptionDef
                            {
                                Loop = true,
                                Restart = false,
                                MaxDistance = 5000,
                                MaxDuration = 1,
                                Scale = 0.25f,
                            },
                        },
                        Hit = new ParticleDef
                        {
                            Name = "",
                            ApplyToShield = true,
                            ShrinkByDistance = true,
                            Color = Color(red: 3, green: 2, blue: 1, alpha: 1),
                            Offset = Vector(x: 0, y: 0, z: 0),
                            Extras = new ParticleOptionDef
                            {
                                Loop = false,
                                Restart = false,
                                MaxDistance = 500,
                                MaxDuration = 1,
                                Scale = .25f,
                                HitPlayChance = 1f,
                            },
                        },
                    },
                    Lines = new LineDef
                    {
                        TracerMaterial = "ProjectileTrailLine", // Particle that follows your projectile. WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                        ColorVariance = Random(start: 5f, end: 10f), // multiply the color by random values within range.
                        WidthVariance = Random(start: 0f, end: 0.045f), // adds random value to default width (negatives shrinks width)
                        Tracer = new TracerBaseDef
                        {
                            Enable = true,
                            Length = .001f,
                            Width = .001f,
                            Color = Color(red: 0.2f, green: 0.9f, blue: 0.9f, alpha: 1),
                        },
                        Trail = new TrailDef
                        {
                            Enable = true,
                            Material = "WeaponLaser",
                            DecayTime = 2,
                            Color = Color(red: 115f, green: 90f, blue: 50f, alpha: 1),
                            Back = true,
                            CustomWidth = .1f,
                            UseWidthVariance = false,
                            UseColorFade = true,
                        },
                        OffsetEffect = new OffsetEffectDef
                        {
                            MaxOffset =  0,// 0 offset value disables this effect
                            MinLength = 0.2f,
                            MaxLength = 3,
                        },
                    },
                },
                AmmoAudio = new AmmoAudioDef
                {
                    TravelSound = "",
                    HitSound = "",
                    HitPlayChance = 0.1f,
                    HitPlayShield = true,
                }, // Don't edit below this line

        };
		private AmmoDef LotusMine2 => new AmmoDef
        {
				AmmoMagazine = "LotusMine", //Name of your ammo type definied in your ammo.sbc file.
                AmmoRound = "LotusMineRepel", //Desired model for the projectiles, must be defined in your ammo.sbc file.
                HybridRound = false, //AmmoMagazine based weapon with energy cost
                EnergyCost = 0.00000000001f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
                BaseDamage = 20f, //Think of this as level of penetration, low base damage for no penetration. Every Block destroyed takes their HP off the projectile.
                Mass = 100f, // in kilograms, determines the amount of kinetic energy applied to the grid being hit.
                Health = 1, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying. KEEP THIS LOW. 1=Standard,10=Insane
                BackKickForce = 1000f, //Determines the amount of kinetic energy applied to the grid firing.
				DecayPerShot = 0f,
				HardPointUsable = false, // set to false if this is a Fragment ammoType and you don't want the turret to be able to select it directly.
				EnergyMagazineSize = 12, //ONLY USE IF "AmmoMagazine=ENERGY". Otherwise specify magazine size in Ammo.sbc file.
				IgnoreWater = false,

                Shape = new ShapeDef //defines the collision shape of projectile, defaults line and visual Line Length if set to 0
                {
                    Shape = LineShape,
                    Diameter = 1,
                },
                ObjectsHit = new ObjectsHitDef
                {
                    MaxObjectsHit = 0, // 0 = disabled
                    CountBlocks = false, // counts gridBlocks and not just entities hit
                },
                Fragment = new FragmentDef
				{
					AmmoRound = "",
					Fragments = 20, //Number of Fragment projectiles created on impact.
					Degrees = 360,  //Think of this as a cone in front of the projectile, 360 degrees for a full circle.
					Reverse = false, //Redirects the cone back towards the projectile's rear
					RandomizeDir = false,
				},
                DamageScales = new DamageScaleDef
                {
                    MaxIntegrity = 0f, // 0 = disabled, 1000 = any blocks with currently integrity above 1000 will be immune to damage.
                    DamageVoxels = false, // true = voxels are vulnerable to this weapon
                    SelfDamage = false, // true = allow self damage.
                    HealthHitModifier = 1, // defaults to a value of 1, this setting modifies how much Health is subtracted from a projectile per hit (1 = per hit).
                    VoxelHitModifier = 3,  // modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01 = 1% damage, 2 = 200% damage.
                    Characters = 0.2f, //Damage to characters. 1f=same as damage to grids
                    Grids = new GridSizeDef
                    {
                        Large = -1f,
                        Small = -1f,
                    },
                    Armor = new ArmorDef
                    {
                        Armor = -1f,
                        Light = -1f,
                        Heavy = -1f,
                        NonArmor = 1f,
                    },
                    Shields = new ShieldDef
                    {
                        Modifier = .75f,
                        Type = Default,
                        BypassModifier = -1f,
                    },
                    // first true/false (ignoreOthers) will cause projectiles to pass through all blocks that do not match the custom subtypeIds.
                    Custom = new CustomScalesDef
                    {
                        IgnoreAllOthers = false,
                        Types = new []
                        {
                            new CustomBlocksDef
                            {
                                SubTypeId = "Test1",
                                Modifier = -1f,
                            },
                            new CustomBlocksDef
                            {
                                SubTypeId = "Test2",
                                Modifier = -1f,
                            },
                        },
                    },
                },
					AreaEffect = new AreaDamageDef
					{
						AreaEffect = PushField, // Disabled = do not use area effect at all, Explosive, Radiant, AntiSmart, JumpNullField, JumpNullField, EnergySinkField, AnchorField, EmpField, OffenseField, NavField, DotField.
						Base = new AreaInfluence
						{
							Radius = 50f, // the sphere of influence of area effects
							EffectStrength = 100000f, // For ewar it applies this amount per pulse/hit, non-ewar applies this as damage per tick per entity in area of influence. For radiant 0 == use spillover from BaseDamage, otherwise use this value.
						},
						Pulse = new PulseDef // interval measured in game ticks (60 == 1 second), pulseChance chance (0 - 100) that an entity in field will be hit
						{
							Interval = 0,
							PulseChance = 100,
							GrowTime = 1,
							HideModel = false,
							ShowParticle = true,
							Particle = new ParticleDef
							{
								Name = "", //ShipWelderArc
								ShrinkByDistance = false,
								Color = Color(red: 128, green: 0, blue: 0, alpha: 32),
								Offset = Vector(x: 0, y: -1, z: 0),
								Extras = new ParticleOptionDef
								{
									Loop = false,
									Restart = false,
									MaxDistance = 5000,
									MaxDuration = 1,
									Scale = 1,
								},
							},
						},
						Explosions = new ExplosionDef
						{
							NoVisuals = false,
							NoSound = false,
							NoShrapnel = false,
							NoDeformation = false,
							Scale = 3,
							CustomParticle = "",
							CustomSound = "ArcWepLrgWarheadExpl",
						},
						Detonation = new DetonateDef
						{
							DetonateOnEnd = true,
							ArmOnlyOnHit = false,
							DetonationDamage = 7500f,
							DetonationRadius = 25f,
							MinArmingTime = 0, //Min time in ticks before projectile will arm for detonation (will also affect Fragment spawning)
						},
						EwarFields = new EwarFieldsDef
						{
							Duration = 6000,
							StackDuration = false,
							Depletable = false,
							MaxStacks = 1,
							TriggerRange = 50f,
							DisableParticleEffect = false,
							Force = new PushPullDef // AreaEffectDamage is multiplied by target mass.
							{
								ForceFrom = ProjectileOrigin, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
								ForceTo = HitPosition, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
								Position = TargetCenterOfMass, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
							},
						},
					},
					Beams = new BeamDef
					{
						Enable = false,
						VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
						ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
						RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
						OneParticle = false, // Only spawn one particle hit per beam weapon.
					},
					Trajectory = new TrajectoryDef
					{
						Guidance = DetectFixed,
						TargetLossDegree = 80f,
						TargetLossTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
						MaxLifeTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
						AccelPerSec = 1f,
						DesiredSpeed = 0, // DO NOT SET HIGHER THAN 4100
						MaxTrajectory = 0f,
						FieldTime = 6000, // 0 is disabled, a value causes the projectile to come to rest, spawn a field and remain for a time (Measured in game ticks, 60 = 1 second)
						GravityMultiplier = 0f, // Gravity multiplier, influences the trajectory of the projectile, value greater than 0 to enable.
						SpeedVariance = Random(start: 0, end: 0), // subtracts value from DesiredSpeed
						RangeVariance = Random(start: 0, end: 0), // subtracts value from MaxTrajectory
						MaxTrajectoryTime = 0, // How long the weapon must fire before it reaches MaxTrajectory.
						Smarts = new SmartsDef
						{
							Inaccuracy = 0f, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
							Aggressiveness = 1f, // controls how responsive tracking is.
							MaxLateralThrust = 0.5, // controls how sharp the trajectile may turn
							TrackingDelay = 0, // Measured in Shape diameter units traveled.
							MaxChaseTime = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
							OverideTarget = false, // when set to true ammo picks its own target, does not use hardpoint's.
							MaxTargets = 0, // Number of targets allowed before ending, 0 = unlimited
							NoTargetExpire = false, // Expire without ever having a target at TargetLossTime
							Roam = false, // Roam current area after target loss
							KeepAliveAfterTargetLoss = true, // Whether to stop early death of projectile on target loss
						},
						Mines = new MinesDef
						{
							DetectRadius = 5f,
							DeCloakRadius = 0f,
							FieldTime = 6000,
							Cloak = false,
							Persist = true,
						},
					},
                AmmoGraphics = new GraphicDef
                {
                    ModelName = "\\Models\\Akiad\\Small\\LotusMine.mwm", //USE DOUBLE LINES, ex: "\\Models\\Name\\Small\\MyGun.mwm"
                    VisualProbability = 1f, //Percentage chance for the model to spawn, "1f" for 100% chance.
                    ShieldHitDraw = true,
                    Particles = new AmmoParticleDef
                    {
                        Ammo = new ParticleDef
                        {
                            Name = "Lotus", //Particle that surrounds your projectile. ShipWelderArc
                            Color = Color(red: 1f, green: 1f, blue: 1f, alpha: 1),
                            Offset = Vector(x: 0, y: 0, z: 0),
                            Extras = new ParticleOptionDef
                            {
                                Loop = true,
                                Restart = false,
                                MaxDistance = 5000,
                                MaxDuration = 1,
                                Scale = 0.25f,
                            },
                        },
                        Hit = new ParticleDef
                        {
                            Name = "",
                            ApplyToShield = true,
                            ShrinkByDistance = true,
                            Color = Color(red: 3, green: 2, blue: 1, alpha: 1),
                            Offset = Vector(x: 0, y: 0, z: 0),
                            Extras = new ParticleOptionDef
                            {
                                Loop = false,
                                Restart = false,
                                MaxDistance = 500,
                                MaxDuration = 1,
                                Scale = .25f,
                                HitPlayChance = 1f,
                            },
                        },
                    },
                    Lines = new LineDef
                    {
                        TracerMaterial = "ProjectileTrailLine", // Particle that follows your projectile. WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                        ColorVariance = Random(start: 5f, end: 10f), // multiply the color by random values within range.
                        WidthVariance = Random(start: 0f, end: 0.045f), // adds random value to default width (negatives shrinks width)
                        Tracer = new TracerBaseDef
                        {
                            Enable = true,
                            Length = .001f,
                            Width = .001f,
                            Color = Color(red: 0.2f, green: 0.9f, blue: 0.9f, alpha: 1),
                        },
                        Trail = new TrailDef
                        {
                            Enable = true,
                            Material = "WeaponLaser",
                            DecayTime = 2,
                            Color = Color(red: 115f, green: 90f, blue: 50f, alpha: 1),
                            Back = true,
                            CustomWidth = .1f,
                            UseWidthVariance = false,
                            UseColorFade = true,
                        },
                        OffsetEffect = new OffsetEffectDef
                        {
                            MaxOffset =  0,// 0 offset value disables this effect
                            MinLength = 0.2f,
                            MaxLength = 3,
                        },
                    },
                },
                AmmoAudio = new AmmoAudioDef
                {
                    TravelSound = "",
                    HitSound = "",
                    HitPlayChance = 0.1f,
                    HitPlayShield = true,
                }, // Don't edit below this line

        };
		private AmmoDef LotusMissile3 => new AmmoDef
        {
				AmmoMagazine = "LotusMissile", //Name of your ammo type definied in your ammo.sbc file.
                AmmoRound = "Explosive", //Desired model for the projectiles, must be defined in your ammo.sbc file.
                HybridRound = false, //AmmoMagazine based weapon with energy cost
                EnergyCost = 0.00000000001f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
                BaseDamage = 0f, //Think of this as level of penetration, low base damage for no penetration. Every Block destroyed takes their HP off the projectile.
                Mass = 100f, // in kilograms, determines the amount of kinetic energy applied to the grid being hit.
                Health = 1, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying. KEEP THIS LOW. 1=Standard,10=Insane
                BackKickForce = 1000f, //Determines the amount of kinetic energy applied to the grid firing.
				DecayPerShot = 0f,
				HardPointUsable = true, // set to false if this is a Fragment ammoType and you don't want the turret to be able to select it directly.
				EnergyMagazineSize = 12, //ONLY USE IF "AmmoMagazine=ENERGY". Otherwise specify magazine size in Ammo.sbc file.
				IgnoreWater = false,

                Shape = new ShapeDef //defines the collision shape of projectile, defaults line and visual Line Length if set to 0
                {
                    Shape = LineShape,
                    Diameter = 1,
                },
                ObjectsHit = new ObjectsHitDef
                {
                    MaxObjectsHit = 0, // 0 = disabled
                    CountBlocks = false, // counts gridBlocks and not just entities hit
                },
                Fragment = new FragmentDef
				{
					AmmoRound = "LotusMineExp",
					Fragments = 2, //Number of Fragment projectiles created on impact.
					Degrees = 180,  //Think of this as a cone in front of the projectile, 360 degrees for a full circle.
					Reverse = false, //Redirects the cone back towards the projectile's rear
					RandomizeDir = false,
				},
                DamageScales = new DamageScaleDef
                {
                    MaxIntegrity = 0f, // 0 = disabled, 1000 = any blocks with currently integrity above 1000 will be immune to damage.
                    DamageVoxels = false, // true = voxels are vulnerable to this weapon
                    SelfDamage = false, // true = allow self damage.
                    HealthHitModifier = 1, // defaults to a value of 1, this setting modifies how much Health is subtracted from a projectile per hit (1 = per hit).
                    VoxelHitModifier = 3,  // modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01 = 1% damage, 2 = 200% damage.
                    Characters = 0.2f, //Damage to characters. 1f=same as damage to grids
                    Grids = new GridSizeDef
                    {
                        Large = -1f,
                        Small = -1f,
                    },
                    Armor = new ArmorDef
                    {
                        Armor = -1f,
                        Light = -1f,
                        Heavy = -1f,
                        NonArmor = 1f,
                    },
                    Shields = new ShieldDef
                    {
                        Modifier = .75f,
                        Type = Default,
                        BypassModifier = -1f,
                    },
                    // first true/false (ignoreOthers) will cause projectiles to pass through all blocks that do not match the custom subtypeIds.
                    Custom = new CustomScalesDef
                    {
                        IgnoreAllOthers = false,
                        Types = new []
                        {
                            new CustomBlocksDef
                            {
                                SubTypeId = "Test1",
                                Modifier = -1f,
                            },
                            new CustomBlocksDef
                            {
                                SubTypeId = "Test2",
                                Modifier = -1f,
                            },
                        },
                    },
                },
                AreaEffect = new AreaDamageDef
                {
                    AreaEffect = Disabled, // Disabled = do not use area effect at all, Explosive, Radiant, AntiSmart, JumpNullField, JumpNullField, EnergySinkField, AnchorField, EmpField, OffenseField, NavField, DotField.
                    AreaEffectDamage = 1000f, // 0 = DO NOT USE FOR EXPLOSIVES, USE DETONATION SETTINGS INSTEAD
                    AreaEffectRadius = 25f, // DO NOT USE FOR EXPLOSIVES, USE DETONATION SETTINGS INSTEAD
                    Pulse = new PulseDef // interval measured in game ticks (60 == 1 second), pulseChance chance (0 - 100) that an entity in field will be hit
                    {
                        Interval = 30,
                        PulseChance = 0,
                    },
                    Explosions = new ExplosionDef
                    {
                        NoVisuals = false, //Set to false to use vanilla explosion particles, unless specified a custom particle
                        NoSound = false, ////Set to false to use vanilla explosion noises, unless specified a custom sound
                        Scale = 4f, //Scale of the particles.
                        CustomParticle = "",
                        CustomSound = "",
                    },
                    Detonation = new DetonateDef //USE THESE MODIFIERS FOR EXPLOSIONS
                    {
                        DetonateOnEnd = false,
                        ArmOnlyOnHit = false,
                        DetonationDamage = 3000, //1 Steelplate =100HP, 1 large light armor cube=2500HP. Has a natural falloff based on radius.
                        DetonationRadius = 25, //Measured in meters, how large the explosion should be.
                    },
                    EwarFields = new EwarFieldsDef
                    {
                        Duration = 0,
                        StackDuration = true,
                        Depletable = false,
                        MaxStacks = 10,
                        TriggerRange = 5f,
                    },
                },
                Beams = new BeamDef
                {
                    Enable = false,
                    VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
                    ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
                    RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
                    OneParticle = false, // Only spawn one particle hit per beam weapon.
                },
                Trajectory = new TrajectoryDef
				{
					Guidance = None, //ammo guidance type, options are None, Smart, Remote(not used yet), TravelTo(if tracking travels to target Position, if none tracking travels to max trajectory, good for flak), DetectFixed(mines), DetectSmart(mines), DetectTravelTo(mines)
					TargetLossDegree = 180f,
					TargetLossTime = 90, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
					MaxLifeTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
					AccelPerSec = 100,
					DesiredSpeed = 1000,
					MaxTrajectory = 200f,
					FieldTime = 0, // 0 is disabled, a value causes the projectile to come to rest, spawn a field and remain for a time (Measured in game ticks, 60 = 1 second)
					GravityMultiplier = 0f, // Gravity influences the trajectory of the projectile.
					SpeedVariance = Random(start: 0, end: 0), // subtracts value from DesiredSpeed
					RangeVariance = Random(start: 0, end: 0), // subtracts value from MaxTrajectory
					Smarts = new SmartsDef
					{
						Inaccuracy = 2f, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
						Aggressiveness = 3f, // controls how responsive tracking is.
						MaxLateralThrust = 0.5, // controls how sharp the trajectile may turn
						TrackingDelay = 200, // Measured in Shape diameter units traveled.
						MaxChaseTime = 2400, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
						OverideTarget = false, // when set to true ammo picks its own target, does not use hardpoint's.
						MaxTargets = 1,
					},
					Mines = new MinesDef
					{
						DetectRadius = 200,
						DeCloakRadius = 100,
						FieldTime = 0,
						Cloak = true,
						Persist = false,
					},
				},
                AmmoGraphics = new GraphicDef
                {
                    ModelName = "\\Models\\Akiad\\Small\\LotusMissile.mwm", //USE DOUBLE LINES, ex: "\\Models\\Name\\Small\\MyGun.mwm"
                    VisualProbability = 1f, //Percentage chance for the model to spawn, "1f" for 100% chance.
                    ShieldHitDraw = true,
                    Particles = new AmmoParticleDef
                    {
                        Ammo = new ParticleDef
                        {
                            Name = "", //Particle that surrounds your projectile. ShipWelderArc
                            Color = Color(red: 1f, green: 1f, blue: 1f, alpha: 1),
                            Offset = Vector(x: 0, y: 0, z: 0),
                            Extras = new ParticleOptionDef
                            {
                                Loop = true,
                                Restart = false,
                                MaxDistance = 5000,
                                MaxDuration = 1,
                                Scale = 0.25f,
                            },
                        },
                        Hit = new ParticleDef
                        {
                            Name = "",
                            ApplyToShield = true,
                            ShrinkByDistance = true,
                            Color = Color(red: 3, green: 2, blue: 1, alpha: 1),
                            Offset = Vector(x: 0, y: 0, z: 0),
                            Extras = new ParticleOptionDef
                            {
                                Loop = false,
                                Restart = false,
                                MaxDistance = 500,
                                MaxDuration = 1,
                                Scale = .25f,
                                HitPlayChance = 1f,
                            },
                        },
                    },
                    Lines = new LineDef
                    {
                        TracerMaterial = "ProjectileTrailLine", // Particle that follows your projectile. WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                        ColorVariance = Random(start: 5f, end: 10f), // multiply the color by random values within range.
                        WidthVariance = Random(start: 0f, end: 0.045f), // adds random value to default width (negatives shrinks width)
                        Tracer = new TracerBaseDef
                        {
                            Enable = true,
                            Length = .001f,
                            Width = .001f,
                            Color = Color(red: 0.2f, green: 0.9f, blue: 0.9f, alpha: 1),
                        },
                        Trail = new TrailDef
                        {
                            Enable = true,
                            Material = "WeaponLaser",
                            DecayTime = 2,
                            Color = Color(red: 115f, green: 90f, blue: 50f, alpha: 1),
                            Back = true,
                            CustomWidth = .1f,
                            UseWidthVariance = false,
                            UseColorFade = true,
                        },
                        OffsetEffect = new OffsetEffectDef
                        {
                            MaxOffset =  0,// 0 offset value disables this effect
                            MinLength = 0.2f,
                            MaxLength = 3,
                        },
                    },
                },
                AmmoAudio = new AmmoAudioDef
                {
                    TravelSound = "",
                    HitSound = "",
                    HitPlayChance = 0.1f,
                    HitPlayShield = true,
                }, // Don't edit below this line

        };
		private AmmoDef LotusMine3 => new AmmoDef
        {
				AmmoMagazine = "LotusMine", //Name of your ammo type definied in your ammo.sbc file.
                AmmoRound = "LotusMineExp", //Desired model for the projectiles, must be defined in your ammo.sbc file.
                HybridRound = false, //AmmoMagazine based weapon with energy cost
                EnergyCost = 0.00000000001f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
                BaseDamage = 20f, //Think of this as level of penetration, low base damage for no penetration. Every Block destroyed takes their HP off the projectile.
                Mass = 100f, // in kilograms, determines the amount of kinetic energy applied to the grid being hit.
                Health = 1, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying. KEEP THIS LOW. 1=Standard,10=Insane
                BackKickForce = 1000f, //Determines the amount of kinetic energy applied to the grid firing.
				DecayPerShot = 0f,
				HardPointUsable = false, // set to false if this is a Fragment ammoType and you don't want the turret to be able to select it directly.
				EnergyMagazineSize = 12, //ONLY USE IF "AmmoMagazine=ENERGY". Otherwise specify magazine size in Ammo.sbc file.
				IgnoreWater = false,

                Shape = new ShapeDef //defines the collision shape of projectile, defaults line and visual Line Length if set to 0
                {
                    Shape = LineShape,
                    Diameter = 1,
                },
                ObjectsHit = new ObjectsHitDef
                {
                    MaxObjectsHit = 0, // 0 = disabled
                    CountBlocks = false, // counts gridBlocks and not just entities hit
                },
                Fragment = new FragmentDef
				{
					AmmoRound = "",
					Fragments = 20, //Number of Fragment projectiles created on impact.
					Degrees = 360,  //Think of this as a cone in front of the projectile, 360 degrees for a full circle.
					Reverse = false, //Redirects the cone back towards the projectile's rear
					RandomizeDir = false,
				},
                DamageScales = new DamageScaleDef
                {
                    MaxIntegrity = 0f, // 0 = disabled, 1000 = any blocks with currently integrity above 1000 will be immune to damage.
                    DamageVoxels = false, // true = voxels are vulnerable to this weapon
                    SelfDamage = false, // true = allow self damage.
                    HealthHitModifier = 1, // defaults to a value of 1, this setting modifies how much Health is subtracted from a projectile per hit (1 = per hit).
                    VoxelHitModifier = 3,  // modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01 = 1% damage, 2 = 200% damage.
                    Characters = 0.2f, //Damage to characters. 1f=same as damage to grids
                    Grids = new GridSizeDef
                    {
                        Large = -1f,
                        Small = -1f,
                    },
                    Armor = new ArmorDef
                    {
                        Armor = -1f,
                        Light = -1f,
                        Heavy = -1f,
                        NonArmor = 1f,
                    },
                    Shields = new ShieldDef
                    {
                        Modifier = .75f,
                        Type = Default,
                        BypassModifier = -1f,
                    },
                    // first true/false (ignoreOthers) will cause projectiles to pass through all blocks that do not match the custom subtypeIds.
                    Custom = new CustomScalesDef
                    {
                        IgnoreAllOthers = false,
                        Types = new []
                        {
                            new CustomBlocksDef
                            {
                                SubTypeId = "Test1",
                                Modifier = -1f,
                            },
                            new CustomBlocksDef
                            {
                                SubTypeId = "Test2",
                                Modifier = -1f,
                            },
                        },
                    },
                },
					AreaEffect = new AreaDamageDef
					{
						AreaEffect = Explosive, // Disabled = do not use area effect at all, Explosive, Radiant, AntiSmart, JumpNullField, JumpNullField, EnergySinkField, AnchorField, EmpField, OffenseField, NavField, DotField.
						Base = new AreaInfluence
						{
							Radius = 25f, // the sphere of influence of area effects
							EffectStrength = 1000f, // For ewar it applies this amount per pulse/hit, non-ewar applies this as damage per tick per entity in area of influence. For radiant 0 == use spillover from BaseDamage, otherwise use this value.
						},
						Pulse = new PulseDef // interval measured in game ticks (60 == 1 second), pulseChance chance (0 - 100) that an entity in field will be hit
						{
							Interval = 0,
							PulseChance = 100,
							GrowTime = 1,
							HideModel = false,
							ShowParticle = true,
							Particle = new ParticleDef
							{
								Name = "", //ShipWelderArc
								ShrinkByDistance = false,
								Color = Color(red: 128, green: 0, blue: 0, alpha: 32),
								Offset = Vector(x: 0, y: -1, z: 0),
								Extras = new ParticleOptionDef
								{
									Loop = false,
									Restart = false,
									MaxDistance = 5000,
									MaxDuration = 1,
									Scale = 1,
								},
							},
						},
						Explosions = new ExplosionDef
						{
							NoVisuals = false,
							NoSound = false,
							NoShrapnel = false,
							NoDeformation = false,
							Scale = 3,
							CustomParticle = "",
							CustomSound = "ArcWepLrgWarheadExpl",
						},
						Detonation = new DetonateDef
						{
							DetonateOnEnd = false,
							ArmOnlyOnHit = false,
							DetonationDamage = 100f,
							DetonationRadius = 25f,
							MinArmingTime = 0, //Min time in ticks before projectile will arm for detonation (will also affect Fragment spawning)
						},
						EwarFields = new EwarFieldsDef
						{
							Duration = 6000,
							StackDuration = false,
							Depletable = false,
							MaxStacks = 1,
							TriggerRange = 50f,
							DisableParticleEffect = false,
							Force = new PushPullDef // AreaEffectDamage is multiplied by target mass.
							{
								ForceFrom = ProjectileOrigin, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
								ForceTo = HitPosition, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
								Position = TargetCenterOfMass, // ProjectileLastPosition, ProjectileOrigin, HitPosition, TargetCenter, TargetCenterOfMass
							},
						},
					},
					Beams = new BeamDef
					{
						Enable = false,
						VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
						ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
						RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
						OneParticle = false, // Only spawn one particle hit per beam weapon.
					},
					Trajectory = new TrajectoryDef
					{
						Guidance = DetectFixed,
						TargetLossDegree = 80f,
						TargetLossTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
						MaxLifeTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
						AccelPerSec = 1f,
						DesiredSpeed = 0, // DO NOT SET HIGHER THAN 4100
						MaxTrajectory = 0f,
						FieldTime = 6000, // 0 is disabled, a value causes the projectile to come to rest, spawn a field and remain for a time (Measured in game ticks, 60 = 1 second)
						GravityMultiplier = 0f, // Gravity multiplier, influences the trajectory of the projectile, value greater than 0 to enable.
						SpeedVariance = Random(start: 0, end: 0), // subtracts value from DesiredSpeed
						RangeVariance = Random(start: 0, end: 0), // subtracts value from MaxTrajectory
						MaxTrajectoryTime = 0, // How long the weapon must fire before it reaches MaxTrajectory.
						Smarts = new SmartsDef
						{
							Inaccuracy = 0f, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
							Aggressiveness = 1f, // controls how responsive tracking is.
							MaxLateralThrust = 0.5, // controls how sharp the trajectile may turn
							TrackingDelay = 0, // Measured in Shape diameter units traveled.
							MaxChaseTime = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
							OverideTarget = false, // when set to true ammo picks its own target, does not use hardpoint's.
							MaxTargets = 0, // Number of targets allowed before ending, 0 = unlimited
							NoTargetExpire = false, // Expire without ever having a target at TargetLossTime
							Roam = false, // Roam current area after target loss
							KeepAliveAfterTargetLoss = true, // Whether to stop early death of projectile on target loss
						},
						Mines = new MinesDef
						{
							DetectRadius = 25f,
							DeCloakRadius = 0f,
							FieldTime = 6000,
							Cloak = false,
							Persist = true,
						},
					},
                AmmoGraphics = new GraphicDef
                {
                    ModelName = "\\Models\\Akiad\\Small\\LotusMine.mwm", //USE DOUBLE LINES, ex: "\\Models\\Name\\Small\\MyGun.mwm"
                    VisualProbability = 1f, //Percentage chance for the model to spawn, "1f" for 100% chance.
                    ShieldHitDraw = true,
                    Particles = new AmmoParticleDef
                    {
                        Ammo = new ParticleDef
                        {
                            Name = "Lotus3", //Particle that surrounds your projectile. ShipWelderArc
                            Color = Color(red: 1f, green: 1f, blue: 1f, alpha: 1),
                            Offset = Vector(x: 0, y: 0, z: 0),
                            Extras = new ParticleOptionDef
                            {
                                Loop = true,
                                Restart = false,
                                MaxDistance = 5000,
                                MaxDuration = 1,
                                Scale = 0.25f,
                            },
                        },
                        Hit = new ParticleDef
                        {
                            Name = "",
                            ApplyToShield = true,
                            ShrinkByDistance = true,
                            Color = Color(red: 3, green: 2, blue: 1, alpha: 1),
                            Offset = Vector(x: 0, y: 0, z: 0),
                            Extras = new ParticleOptionDef
                            {
                                Loop = false,
                                Restart = false,
                                MaxDistance = 500,
                                MaxDuration = 1,
                                Scale = .25f,
                                HitPlayChance = 1f,
                            },
                        },
                    },
                    Lines = new LineDef
                    {
                        TracerMaterial = "ProjectileTrailLine", // Particle that follows your projectile. WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                        ColorVariance = Random(start: 5f, end: 10f), // multiply the color by random values within range.
                        WidthVariance = Random(start: 0f, end: 0.045f), // adds random value to default width (negatives shrinks width)
                        Tracer = new TracerBaseDef
                        {
                            Enable = true,
                            Length = .001f,
                            Width = .001f,
                            Color = Color(red: 0.2f, green: 0.9f, blue: 0.9f, alpha: 1),
                        },
                        Trail = new TrailDef
                        {
                            Enable = true,
                            Material = "WeaponLaser",
                            DecayTime = 2,
                            Color = Color(red: 115f, green: 90f, blue: 50f, alpha: 1),
                            Back = true,
                            CustomWidth = .1f,
                            UseWidthVariance = false,
                            UseColorFade = true,
                        },
                        OffsetEffect = new OffsetEffectDef
                        {
                            MaxOffset =  0,// 0 offset value disables this effect
                            MinLength = 0.2f,
                            MaxLength = 3,
                        },
                    },
                },
                AmmoAudio = new AmmoAudioDef
                {
                    TravelSound = "",
                    HitSound = "",
                    HitPlayChance = 0.1f,
                    HitPlayShield = true,
                }, // Don't edit below this line

        };
		
		private AmmoDef CycloneBomb => new AmmoDef
        {
				AmmoMagazine = "CycloneBomb", //Name of your ammo type definied in your ammo.sbc file.
                AmmoRound = "Scylla", //Desired model for the projectiles, must be defined in your ammo.sbc file.
                HybridRound = false, //AmmoMagazine based weapon with energy cost
                EnergyCost = 0.00000000001f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
                BaseDamage = 1000000f, //Think of this as level of penetration, low base damage for no penetration. Every Block destroyed takes their HP off the projectile.
                Mass = 100000000000f, // in kilograms, determines the amount of kinetic energy applied to the grid being hit.
                Health = 1, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying. KEEP THIS LOW. 1=Standard,10=Insane
                BackKickForce = 1000f, //Determines the amount of kinetic energy applied to the grid firing.
				DecayPerShot = 0f,
				HardPointUsable = true, // set to false if this is a Fragment ammoType and you don't want the turret to be able to select it directly.
				EnergyMagazineSize = 12, //ONLY USE IF "AmmoMagazine=ENERGY". Otherwise specify magazine size in Ammo.sbc file.
				IgnoreWater = false,

                Shape = new ShapeDef //defines the collision shape of projectile, defaults line and visual Line Length if set to 0
                {
                    Shape = LineShape,
                    Diameter = 1,
                },
                ObjectsHit = new ObjectsHitDef
                {
                    MaxObjectsHit = 0, // 0 = disabled
                    CountBlocks = false, // counts gridBlocks and not just entities hit
                },
                Fragment = new FragmentDef
				{
					AmmoRound = "HEShrapnel",
					Fragments = 10, //Number of Fragment projectiles created on impact.
					Degrees = 180,  //Think of this as a cone in front of the projectile, 360 degrees for a full circle.
					Reverse = false, //Redirects the cone back towards the projectile's rear
					RandomizeDir = false,
				},
                DamageScales = new DamageScaleDef
                {
                    MaxIntegrity = 0f, // 0 = disabled, 1000 = any blocks with currently integrity above 1000 will be immune to damage.
                    DamageVoxels = true, // true = voxels are vulnerable to this weapon
                    SelfDamage = false, // true = allow self damage.
                    HealthHitModifier = 100, // defaults to a value of 1, this setting modifies how much Health is subtracted from a projectile per hit (1 = per hit).
                    VoxelHitModifier = 1,  // modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01 = 1% damage, 2 = 200% damage.
                    Characters = 2f, //Damage to characters. 1f=same as damage to grids
                    Grids = new GridSizeDef
                    {
                        Large = 1f,
                        Small = 1f,
                    },
                    Armor = new ArmorDef
                    {
                        Armor = 1f,
                        Light = 1f,
                        Heavy = 1f,
                        NonArmor = 1f,
                    },
                    Shields = new ShieldDef
                    {
                        Modifier = .75f,
                        Type = Default,
                        BypassModifier = -1f,
                    },
                    // first true/false (ignoreOthers) will cause projectiles to pass through all blocks that do not match the custom subtypeIds.
                    Custom = new CustomScalesDef
                    {
                        IgnoreAllOthers = false,
                        Types = new []
                        {
                            new CustomBlocksDef
                            {
                                SubTypeId = "Test1",
                                Modifier = -1f,
                            },
                            new CustomBlocksDef
                            {
                                SubTypeId = "Test2",
                                Modifier = -1f,
                            },
                        },
                    },
                },
                AreaEffect = new AreaDamageDef
                {
                    AreaEffect = Explosive, // Disabled = do not use area effect at all, Explosive, Radiant, AntiSmart, JumpNullField, JumpNullField, EnergySinkField, AnchorField, EmpField, OffenseField, NavField, DotField.
                    AreaEffectDamage = 0f, // 0 = DO NOT USE FOR EXPLOSIVES, USE DETONATION SETTINGS INSTEAD
                    AreaEffectRadius = 0f, // DO NOT USE FOR EXPLOSIVES, USE DETONATION SETTINGS INSTEAD
                    Pulse = new PulseDef // interval measured in game ticks (60 == 1 second), pulseChance chance (0 - 100) that an entity in field will be hit
                    {
                        Interval = 30,
                        PulseChance = 0,
                    },
                    Explosions = new ExplosionDef
                    {
                        NoVisuals = false, //Set to false to use vanilla explosion particles, unless specified a custom particle
                        NoSound = false, ////Set to false to use vanilla explosion noises, unless specified a custom sound
                        Scale = 5f, //Scale of the particles.
                        CustomParticle = "",
                        CustomSound = "",
                    },
                    Detonation = new DetonateDef //USE THESE MODIFIERS FOR EXPLOSIONS
                    {
                        DetonateOnEnd = true,
                        ArmOnlyOnHit = false,
                        DetonationDamage = 45000, //1 Steelplate =100HP, 1 large light armor cube=2500HP. Has a natural falloff based on radius.
                        DetonationRadius = 60, //Measured in meters, how large the explosion should be.
                    },
                    EwarFields = new EwarFieldsDef
                    {
                        Duration = 0,
                        StackDuration = true,
                        Depletable = false,
                        MaxStacks = 10,
                        TriggerRange = 5f,
                    },
                },
                Beams = new BeamDef
                {
                    Enable = false,
                    VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
                    ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
                    RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
                    OneParticle = false, // Only spawn one particle hit per beam weapon.
                },
                Trajectory = new TrajectoryDef
                {
                    Guidance = None,
                    TargetLossDegree = 180f,
                    TargetLossTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    MaxLifeTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    AccelPerSec = 1f,
                    DesiredSpeed = 500, //Speed of the projectile. DO NOT SET HIGHER THAN 4100.
                    MaxTrajectory = 5000f, //Maximum distance the projectile can travel before detonating/despawning.
                    FieldTime = 0, // 0 is disabled, a value causes the projectile to come to rest, spawn a field and remain for a time (Measured in game ticks, 60 = 1 second)
					GravityMultiplier = 20f, //1f=Gravity applies normal pull to projectile. 0 to disable.
                    SpeedVariance = Random(start: 0, end: 0), // subtracts value from DesiredSpeed
                    RangeVariance = Random(start: 0, end: 0), // subtracts value from MaxTrajectory
                    Smarts = new SmartsDef
                    {
                        Inaccuracy = 0, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
                        Aggressiveness = 3f, // controls how responsive tracking is.
                        MaxLateralThrust = .5f, // controls how sharp the trajectile may turn
                        TrackingDelay = 0, // Measured in Shape diameter units traveled.
                        MaxChaseTime = 2000, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                        OverideTarget = false, // when set to true ammo picks its own target, does not use hardpoint's.
                    },
                    Mines = new MinesDef
                    {
                        DetectRadius =  200,
                        DeCloakRadius = 100,
                        FieldTime = 1800,
                        Cloak = true,
                        Persist = false,
                    },
                },
                AmmoGraphics = new GraphicDef
                {
                    ModelName = "\\Models\\Akiad\\Small\\CycloneBomb.mwm", //USE DOUBLE LINES, ex: "\\Models\\Name\\Small\\MyGun.mwm"
                    VisualProbability = 1f, //Percentage chance for the model to spawn, "1f" for 100% chance.
                    ShieldHitDraw = true,
                    Particles = new AmmoParticleDef
                    {
                        Ammo = new ParticleDef
                        {
                            Name = "Smoke_Missile", //Particle that surrounds your projectile. ShipWelderArc
                            Color = Color(red: 1f, green: 1f, blue: 1f, alpha: 1),
                            Offset = Vector(x: 0, y: 0, z: 0),
                            Extras = new ParticleOptionDef
                            {
                                Loop = true,
                                Restart = false,
                                MaxDistance = 5000,
                                MaxDuration = 1,
                                Scale = 0.25f,
                            },
                        },
                        Hit = new ParticleDef
                        {
                            Name = "CycloneExplosion",
                            ApplyToShield = true,
                            ShrinkByDistance = true,
                            Color = Color(red: 1, green: 1, blue: 5, alpha: 1),
                            Offset = Vector(x: 0, y: 0, z: 0),
                            Extras = new ParticleOptionDef
                            {
                                Loop = false,
                                Restart = false,
                                MaxDistance = 500,
                                MaxDuration = 1,
                                Scale = 1f,
                                HitPlayChance = 1f,
                            },
                        },
                    },
                    Lines = new LineDef
                    {
                        TracerMaterial = "ProjectileTrailLine", // Particle that follows your projectile. WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                        ColorVariance = Random(start: 5f, end: 10f), // multiply the color by random values within range.
                        WidthVariance = Random(start: 0f, end: 0.045f), // adds random value to default width (negatives shrinks width)
                        Tracer = new TracerBaseDef
                        {
                            Enable = false,
                            Length = 3f,
                            Width = 0.05f,
                            Color = Color(red: 0.9f, green: 0.9f, blue: 0.9f, alpha: 1),
                        },
                        Trail = new TrailDef
                        {
                            Enable = false,
                            Material = "WeaponLaser",
                            DecayTime = 128,
                            Color = Color(red: 0, green: 0, blue: 1, alpha: 1),
                            Back = true,
                            CustomWidth = 0,
                            UseWidthVariance = false,
                            UseColorFade = true,
                        },
                        OffsetEffect = new OffsetEffectDef
                        {
                            MaxOffset =  0,// 0 offset value disables this effect
                            MinLength = 0.2f,
                            MaxLength = 3,
                        },
                    },
                },
                AmmoAudio = new AmmoAudioDef
                {
                    TravelSound = "",
                    HitSound = "",
                    HitPlayChance = 0.1f,
                    HitPlayShield = true,
                }, // Don't edit below this line

        };
		private AmmoDef CharybdisBomb => new AmmoDef
        {
				AmmoMagazine = "CharybdisBomb", //Name of your ammo type definied in your ammo.sbc file.
                AmmoRound = "Charybdis", //Desired model for the projectiles, must be defined in your ammo.sbc file.
                HybridRound = false, //AmmoMagazine based weapon with energy cost
                EnergyCost = 0.00000000001f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
                BaseDamage = 100f, //Think of this as level of penetration, low base damage for no penetration. Every Block destroyed takes their HP off the projectile.
                Mass = 10000f, // in kilograms, determines the amount of kinetic energy applied to the grid being hit.
                Health = 1, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying. KEEP THIS LOW. 1=Standard,10=Insane
                BackKickForce = 1000f, //Determines the amount of kinetic energy applied to the grid firing.
				DecayPerShot = 0f,
				HardPointUsable = true, // set to false if this is a Fragment ammoType and you don't want the turret to be able to select it directly.
				EnergyMagazineSize = 12, //ONLY USE IF "AmmoMagazine=ENERGY". Otherwise specify magazine size in Ammo.sbc file.
				IgnoreWater = false,

                Shape = new ShapeDef //defines the collision shape of projectile, defaults line and visual Line Length if set to 0
                {
                    Shape = LineShape,
                    Diameter = 1,
                },
                ObjectsHit = new ObjectsHitDef
                {
                    MaxObjectsHit = 0, // 0 = disabled
                    CountBlocks = false, // counts gridBlocks and not just entities hit
                },
                Fragment = new FragmentDef
				{
					AmmoRound = "CharybdisBombStage2",
					Fragments = 1, //Number of Fragment projectiles created on impact.
					Degrees = 1,  //Think of this as a cone in front of the projectile, 360 degrees for a full circle.
					Reverse = false, //Redirects the cone back towards the projectile's rear
					RandomizeDir = false,
				},
                DamageScales = new DamageScaleDef
                {
                    MaxIntegrity = 0f, // 0 = disabled, 1000 = any blocks with currently integrity above 1000 will be immune to damage.
                    DamageVoxels = false, // true = voxels are vulnerable to this weapon
                    SelfDamage = false, // true = allow self damage.
                    HealthHitModifier = 1, // defaults to a value of 1, this setting modifies how much Health is subtracted from a projectile per hit (1 = per hit).
                    VoxelHitModifier = 3,  // modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01 = 1% damage, 2 = 200% damage.
                    Characters = 0.2f, //Damage to characters. 1f=same as damage to grids
                    Grids = new GridSizeDef
                    {
                        Large = 1f,
                        Small = 1f,
                    },
                    Armor = new ArmorDef
                    {
                        Armor = -1f,
                        Light = -1f,
                        Heavy = -1f,
                        NonArmor = 1f,
                    },
                    Shields = new ShieldDef
                    {
                        Modifier = .75f,
                        Type = Default,
                        BypassModifier = -1f,
                    },
                    // first true/false (ignoreOthers) will cause projectiles to pass through all blocks that do not match the custom subtypeIds.
                    Custom = new CustomScalesDef
                    {
                        IgnoreAllOthers = false,
                        Types = new []
                        {
                            new CustomBlocksDef
                            {
                                SubTypeId = "Test1",
                                Modifier = -1f,
                            },
                            new CustomBlocksDef
                            {
                                SubTypeId = "Test2",
                                Modifier = -1f,
                            },
                        },
                    },
                },
                AreaEffect = new AreaDamageDef
                {
                    AreaEffect = Explosive, // Disabled = do not use area effect at all, Explosive, Radiant, AntiSmart, JumpNullField, JumpNullField, EnergySinkField, AnchorField, EmpField, OffenseField, NavField, DotField.
                    AreaEffectDamage = 0f, // 0 = DO NOT USE FOR EXPLOSIVES, USE DETONATION SETTINGS INSTEAD
                    AreaEffectRadius = 0f, // DO NOT USE FOR EXPLOSIVES, USE DETONATION SETTINGS INSTEAD
                    Pulse = new PulseDef // interval measured in game ticks (60 == 1 second), pulseChance chance (0 - 100) that an entity in field will be hit
                    {
                        Interval = 30,
                        PulseChance = 0,
                    },
                    Explosions = new ExplosionDef
                    {
                        NoVisuals = false, //Set to false to use vanilla explosion particles, unless specified a custom particle
                        NoSound = false, ////Set to false to use vanilla explosion noises, unless specified a custom sound
                        Scale = 1f, //Scale of the particles.
                        CustomParticle = "",
                        CustomSound = "",
                    },
                    Detonation = new DetonateDef //USE THESE MODIFIERS FOR EXPLOSIONS
                    {
                        DetonateOnEnd = false,
                        ArmOnlyOnHit = false,
                        DetonationDamage = 500, //1 Steelplate =100HP, 1 large light armor cube=2500HP. Has a natural falloff based on radius.
                        DetonationRadius = 2, //Measured in meters, how large the explosion should be.
                    },
                    EwarFields = new EwarFieldsDef
                    {
                        Duration = 0,
                        StackDuration = true,
                        Depletable = false,
                        MaxStacks = 10,
                        TriggerRange = 5f,
                    },
                },
                Beams = new BeamDef
                {
                    Enable = false,
                    VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
                    ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
                    RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
                    OneParticle = false, // Only spawn one particle hit per beam weapon.
                },
                Trajectory = new TrajectoryDef
            {
                Guidance = None,
                TargetLossDegree = 90,
                TargetLossTime = 600, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                MaxLifeTime = 90, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                AccelPerSec = 0f,
                DesiredSpeed = 25f,
                MaxTrajectory = 10000f,
                FieldTime = 0, // 0 is disabled, a value causes the projectile to come to rest, spawn a field and remain for a time (Measured in game ticks, 60 = 1 second)
                GravityMultiplier = 0f, // Gravity multiplier, influences the trajectory of the projectile, value greater than 0 to enable.
                SpeedVariance = Random(start: 0, end: 0), // subtracts value from DesiredSpeed
                RangeVariance = Random(start: 0, end: 0), // subtracts value from MaxTrajectory
                MaxTrajectoryTime = 0, // How long the weapon must fire before it reaches MaxTrajectory.
                Smarts = new SmartsDef
                {
                    Inaccuracy = 50f, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
                    Aggressiveness = 2f, // controls how responsive tracking is.
                    MaxLateralThrust = .49f, // controls how sharp the trajectile may turn
                    TrackingDelay = 20, // Measured in Shape diameter units traveled.
                    MaxChaseTime = 2400, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    OverideTarget = true, // when set to true ammo picks its own target, does not use hardpoint's.
                    MaxTargets = 8, // Number of targets allowed before ending, 0 = unlimited
                    NoTargetExpire = false, // Expire without ever having a target at TargetLossTime
                    Roam = false, // Roam current area after target loss
                },
                Mines = new MinesDef
                {
                    DetectRadius = 0,
                    DeCloakRadius = 0,
                    FieldTime = 0,
                    Cloak = false,
                    Persist = false,
                },
            },
                AmmoGraphics = new GraphicDef
                {
                    ModelName = "\\Models\\Akiad\\Small\\CharybdisBomb.mwm", //USE DOUBLE LINES, ex: "\\Models\\Name\\Small\\MyGun.mwm"
                    VisualProbability = 1f, //Percentage chance for the model to spawn, "1f" for 100% chance.
                    ShieldHitDraw = true,
                    Particles = new AmmoParticleDef
                    {
                        Ammo = new ParticleDef
                        {
                            Name = "", //Particle that surrounds your projectile. ShipWelderArc
                            Color = Color(red: 1f, green: 1f, blue: 1f, alpha: 1),
                            Offset = Vector(x: 0, y: 0, z: 0),
                            Extras = new ParticleOptionDef
                            {
                                Loop = true,
                                Restart = false,
                                MaxDistance = 5000,
                                MaxDuration = 1,
                                Scale = 0.25f,
                            },
                        },
                        Hit = new ParticleDef
                        {
                            Name = "CycloneExplosion",
                            ApplyToShield = true,
                            ShrinkByDistance = true,
                            Color = Color(red: 1, green: 1, blue: 5, alpha: 1),
                            Offset = Vector(x: 0, y: 0, z: 0),
                            Extras = new ParticleOptionDef
                            {
                                Loop = false,
                                Restart = false,
                                MaxDistance = 500,
                                MaxDuration = 1,
                                Scale = 1f,
                                HitPlayChance = 1f,
                            },
                        },
                    },
                    Lines = new LineDef
                    {
                        TracerMaterial = "ProjectileTrailLine", // Particle that follows your projectile. WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                        ColorVariance = Random(start: 5f, end: 10f), // multiply the color by random values within range.
                        WidthVariance = Random(start: 0f, end: 0.045f), // adds random value to default width (negatives shrinks width)
                        Tracer = new TracerBaseDef
                        {
                            Enable = true,
                            Length = .001f,
                            Width = .001f,
                            Color = Color(red: 0.9f, green: 0.9f, blue: 20f, alpha: 1),
                        },
                        Trail = new TrailDef
                        {
                            Enable = true,
                            Material = "WeaponLaser",
                            DecayTime = 6,
                            Color = Color(red: 5, green: 5, blue: 25, alpha: 1),
                            Back = true,
                            CustomWidth = .05f,
                            UseWidthVariance = false,
                            UseColorFade = true,
                        },
                        OffsetEffect = new OffsetEffectDef
                        {
                            MaxOffset =  0,// 0 offset value disables this effect
                            MinLength = 0.2f,
                            MaxLength = 3,
                        },
                    },
                },
                AmmoAudio = new AmmoAudioDef
                {
                    TravelSound = "",
                    HitSound = "",
                    HitPlayChance = 0.1f,
                    HitPlayShield = true,
                }, // Don't edit below this line

        };
		private AmmoDef CharybdisBombStage2 => new AmmoDef
        {
				AmmoMagazine = "CharybdisBombStage2", //Name of your ammo type definied in your ammo.sbc file.
                AmmoRound = "CharybdisBombStage2", //Desired model for the projectiles, must be defined in your ammo.sbc file.
                HybridRound = false, //AmmoMagazine based weapon with energy cost
                EnergyCost = 0.00000000001f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
                BaseDamage = 1f, //Think of this as level of penetration, low base damage for no penetration. Every Block destroyed takes their HP off the projectile.
                Mass = 10f, // in kilograms, determines the amount of kinetic energy applied to the grid being hit.
                Health = 1, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying. KEEP THIS LOW. 1=Standard,10=Insane
                BackKickForce = 1000f, //Determines the amount of kinetic energy applied to the grid firing.
				DecayPerShot = 0f,
				HardPointUsable = false, // set to false if this is a Fragment ammoType and you don't want the turret to be able to select it directly.
				EnergyMagazineSize = 12, //ONLY USE IF "AmmoMagazine=ENERGY". Otherwise specify magazine size in Ammo.sbc file.
				IgnoreWater = false,

                Shape = new ShapeDef //defines the collision shape of projectile, defaults line and visual Line Length if set to 0
                {
                    Shape = LineShape,
                    Diameter = 1,
                },
                ObjectsHit = new ObjectsHitDef
                {
                    MaxObjectsHit = 0, // 0 = disabled
                    CountBlocks = false, // counts gridBlocks and not just entities hit
                },
                Fragment = new FragmentDef
				{
					AmmoRound = "CharybdisBombStage3",
					Fragments = 1, //Number of Fragment projectiles created on impact.
					Degrees = 1,  //Think of this as a cone in front of the projectile, 360 degrees for a full circle.
					Reverse = false, //Redirects the cone back towards the projectile's rear
					RandomizeDir = false,
				},
                DamageScales = new DamageScaleDef
                {
                    MaxIntegrity = 0f, // 0 = disabled, 1000 = any blocks with currently integrity above 1000 will be immune to damage.
                    DamageVoxels = false, // true = voxels are vulnerable to this weapon
                    SelfDamage = false, // true = allow self damage.
                    HealthHitModifier = 1, // defaults to a value of 1, this setting modifies how much Health is subtracted from a projectile per hit (1 = per hit).
                    VoxelHitModifier = 3,  // modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01 = 1% damage, 2 = 200% damage.
                    Characters = 0.2f, //Damage to characters. 1f=same as damage to grids
                    Grids = new GridSizeDef
                    {
                        Large = -1f,
                        Small = -1f,
                    },
                    Armor = new ArmorDef
                    {
                        Armor = -1f,
                        Light = -1f,
                        Heavy = -1f,
                        NonArmor = 1f,
                    },
                    Shields = new ShieldDef
                    {
                        Modifier = .75f,
                        Type = Default,
                        BypassModifier = -1f,
                    },
                    // first true/false (ignoreOthers) will cause projectiles to pass through all blocks that do not match the custom subtypeIds.
                    Custom = new CustomScalesDef
                    {
                        IgnoreAllOthers = false,
                        Types = new []
                        {
                            new CustomBlocksDef
                            {
                                SubTypeId = "Test1",
                                Modifier = -1f,
                            },
                            new CustomBlocksDef
                            {
                                SubTypeId = "Test2",
                                Modifier = -1f,
                            },
                        },
                    },
                },
                AreaEffect = new AreaDamageDef
                {
                    AreaEffect = Disabled, // Disabled = do not use area effect at all, Explosive, Radiant, AntiSmart, JumpNullField, JumpNullField, EnergySinkField, AnchorField, EmpField, OffenseField, NavField, DotField.
                    AreaEffectDamage = 0f, // 0 = DO NOT USE FOR EXPLOSIVES, USE DETONATION SETTINGS INSTEAD
                    AreaEffectRadius = 0f, // DO NOT USE FOR EXPLOSIVES, USE DETONATION SETTINGS INSTEAD
                    Pulse = new PulseDef // interval measured in game ticks (60 == 1 second), pulseChance chance (0 - 100) that an entity in field will be hit
                    {
                        Interval = 30,
                        PulseChance = 0,
                    },
                    Explosions = new ExplosionDef
                    {
                        NoVisuals = false, //Set to false to use vanilla explosion particles, unless specified a custom particle
                        NoSound = false, ////Set to false to use vanilla explosion noises, unless specified a custom sound
                        Scale = 1f, //Scale of the particles.
                        CustomParticle = "",
                        CustomSound = "",
                    },
                    Detonation = new DetonateDef //USE THESE MODIFIERS FOR EXPLOSIONS
                    {
                        DetonateOnEnd = false,
                        ArmOnlyOnHit = false,
                        DetonationDamage = 500, //1 Steelplate =100HP, 1 large light armor cube=2500HP. Has a natural falloff based on radius.
                        DetonationRadius = 2, //Measured in meters, how large the explosion should be.
                    },
                    EwarFields = new EwarFieldsDef
                    {
                        Duration = 0,
                        StackDuration = true,
                        Depletable = false,
                        MaxStacks = 10,
                        TriggerRange = 5f,
                    },
                },
                Beams = new BeamDef
                {
                    Enable = false,
                    VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
                    ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
                    RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
                    OneParticle = false, // Only spawn one particle hit per beam weapon.
                },
                Trajectory = new TrajectoryDef
            {
                Guidance = Smart,
                TargetLossDegree = 0,
                TargetLossTime = 600, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                MaxLifeTime = 60, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                AccelPerSec = 500f,
                DesiredSpeed = 25,
                MaxTrajectory = 10000f,
                FieldTime = 0, // 0 is disabled, a value causes the projectile to come to rest, spawn a field and remain for a time (Measured in game ticks, 60 = 1 second)
                GravityMultiplier = 0f, // Gravity multiplier, influences the trajectory of the projectile, value greater than 0 to enable.
                SpeedVariance = Random(start: 0, end: 0), // subtracts value from DesiredSpeed
                RangeVariance = Random(start: 0, end: 0), // subtracts value from MaxTrajectory
                MaxTrajectoryTime = 0, // How long the weapon must fire before it reaches MaxTrajectory.
                Smarts = new SmartsDef
                {
                    Inaccuracy = 50f, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
                    Aggressiveness = 2f, // controls how responsive tracking is.
                    MaxLateralThrust = .45f, // controls how sharp the trajectile may turn
                    TrackingDelay = 0, // Measured in Shape diameter units traveled.
                    MaxChaseTime = 2400, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    OverideTarget = true, // when set to true ammo picks its own target, does not use hardpoint's.
                    MaxTargets = 8, // Number of targets allowed before ending, 0 = unlimited
                    NoTargetExpire = false, // Expire without ever having a target at TargetLossTime
                    Roam = false, // Roam current area after target loss
                },
                Mines = new MinesDef
                {
                    DetectRadius = 0,
                    DeCloakRadius = 0,
                    FieldTime = 0,
                    Cloak = false,
                    Persist = false,
                },
            },
                AmmoGraphics = new GraphicDef
                {
                    ModelName = "\\Models\\Akiad\\Small\\CharybdisBomb.mwm", //USE DOUBLE LINES, ex: "\\Models\\Name\\Small\\MyGun.mwm"
                    VisualProbability = 1f, //Percentage chance for the model to spawn, "1f" for 100% chance.
                    ShieldHitDraw = true,
                    Particles = new AmmoParticleDef
                    {
                        Ammo = new ParticleDef
                        {
                            Name = "", //Particle that surrounds your projectile. ShipWelderArc
                            Color = Color(red: 1f, green: 1f, blue: 1f, alpha: 1),
                            Offset = Vector(x: 0, y: 0, z: 0),
                            Extras = new ParticleOptionDef
                            {
                                Loop = true,
                                Restart = false,
                                MaxDistance = 5000,
                                MaxDuration = 1,
                                Scale = 0.25f,
                            },
                        },
                        Hit = new ParticleDef
                        {
                            Name = "CycloneExplosion",
                            ApplyToShield = true,
                            ShrinkByDistance = true,
                            Color = Color(red: 1, green: 1, blue: 5, alpha: 1),
                            Offset = Vector(x: 0, y: 0, z: 0),
                            Extras = new ParticleOptionDef
                            {
                                Loop = false,
                                Restart = false,
                                MaxDistance = 500,
                                MaxDuration = 1,
                                Scale = 1f,
                                HitPlayChance = 1f,
                            },
                        },
                    },
                    Lines = new LineDef
                    {
                        TracerMaterial = "ProjectileTrailLine", // Particle that follows your projectile. WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                        ColorVariance = Random(start: 5f, end: 10f), // multiply the color by random values within range.
                        WidthVariance = Random(start: 0f, end: 0.045f), // adds random value to default width (negatives shrinks width)
                        Tracer = new TracerBaseDef
                        {
                            Enable = true,
                            Length = .001f,
                            Width = .001f,
                            Color = Color(red: 0.9f, green: 0.9f, blue: 20f, alpha: 1),
                        },
                        Trail = new TrailDef
                        {
                            Enable = true,
                            Material = "WeaponLaser",
                            DecayTime = 6,
                            Color = Color(red: 5, green: 5, blue: 25, alpha: 1),
                            Back = true,
                            CustomWidth = .05f,
                            UseWidthVariance = false,
                            UseColorFade = true,
                        },
                        OffsetEffect = new OffsetEffectDef
                        {
                            MaxOffset =  0,// 0 offset value disables this effect
                            MinLength = 0.2f,
                            MaxLength = 3,
                        },
                    },
                },
                AmmoAudio = new AmmoAudioDef
                {
                    TravelSound = "",
                    HitSound = "",
                    HitPlayChance = 0.1f,
                    HitPlayShield = true,
                }, // Don't edit below this line

        };
		private AmmoDef CharybdisBombStage3 => new AmmoDef
        {
				AmmoMagazine = "CharybdisBombStage2", //Name of your ammo type definied in your ammo.sbc file.
                AmmoRound = "CharybdisBombStage3", //Desired model for the projectiles, must be defined in your ammo.sbc file.
                HybridRound = false, //AmmoMagazine based weapon with energy cost
                EnergyCost = 0.00000000001f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
                BaseDamage = 1f, //Think of this as level of penetration, low base damage for no penetration. Every Block destroyed takes their HP off the projectile.
                Mass = 10f, // in kilograms, determines the amount of kinetic energy applied to the grid being hit.
                Health = 1, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying. KEEP THIS LOW. 1=Standard,10=Insane
                BackKickForce = 1000f, //Determines the amount of kinetic energy applied to the grid firing.
				DecayPerShot = 0f,
				HardPointUsable = false, // set to false if this is a Fragment ammoType and you don't want the turret to be able to select it directly.
				EnergyMagazineSize = 12, //ONLY USE IF "AmmoMagazine=ENERGY". Otherwise specify magazine size in Ammo.sbc file.
				IgnoreWater = false,

                Shape = new ShapeDef //defines the collision shape of projectile, defaults line and visual Line Length if set to 0
                {
                    Shape = LineShape,
                    Diameter = 1,
                },
                ObjectsHit = new ObjectsHitDef
                {
                    MaxObjectsHit = 0, // 0 = disabled
                    CountBlocks = false, // counts gridBlocks and not just entities hit
                },
                Fragment = new FragmentDef
				{
					AmmoRound = "HEShrapnel",
					Fragments = 3, //Number of Fragment projectiles created on impact.
					Degrees = 120,  //Think of this as a cone in front of the projectile, 360 degrees for a full circle.
					Reverse = false, //Redirects the cone back towards the projectile's rear
					RandomizeDir = false,
				},
                DamageScales = new DamageScaleDef
                {
                    MaxIntegrity = 0f, // 0 = disabled, 1000 = any blocks with currently integrity above 1000 will be immune to damage.
                    DamageVoxels = false, // true = voxels are vulnerable to this weapon
                    SelfDamage = false, // true = allow self damage.
                    HealthHitModifier = 1, // defaults to a value of 1, this setting modifies how much Health is subtracted from a projectile per hit (1 = per hit).
                    VoxelHitModifier = 3,  // modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01 = 1% damage, 2 = 200% damage.
                    Characters = 0.2f, //Damage to characters. 1f=same as damage to grids
                    Grids = new GridSizeDef
                    {
                        Large = -1f,
                        Small = -1f,
                    },
                    Armor = new ArmorDef
                    {
                        Armor = -1f,
                        Light = -1f,
                        Heavy = -1f,
                        NonArmor = 1f,
                    },
                    Shields = new ShieldDef
                    {
                        Modifier = .75f,
                        Type = Default,
                        BypassModifier = -1f,
                    },
                    // first true/false (ignoreOthers) will cause projectiles to pass through all blocks that do not match the custom subtypeIds.
                    Custom = new CustomScalesDef
                    {
                        IgnoreAllOthers = false,
                        Types = new []
                        {
                            new CustomBlocksDef
                            {
                                SubTypeId = "Test1",
                                Modifier = -1f,
                            },
                            new CustomBlocksDef
                            {
                                SubTypeId = "Test2",
                                Modifier = -1f,
                            },
                        },
                    },
                },
                AreaEffect = new AreaDamageDef
                {
                    AreaEffect = Explosive, // Disabled = do not use area effect at all, Explosive, Radiant, AntiSmart, JumpNullField, JumpNullField, EnergySinkField, AnchorField, EmpField, OffenseField, NavField, DotField.
                    AreaEffectDamage = 0f, // 0 = DO NOT USE FOR EXPLOSIVES, USE DETONATION SETTINGS INSTEAD
                    AreaEffectRadius = 0f, // DO NOT USE FOR EXPLOSIVES, USE DETONATION SETTINGS INSTEAD
                    Pulse = new PulseDef // interval measured in game ticks (60 == 1 second), pulseChance chance (0 - 100) that an entity in field will be hit
                    {
                        Interval = 30,
                        PulseChance = 0,
                    },
                    Explosions = new ExplosionDef
                    {
                        NoVisuals = false, //Set to false to use vanilla explosion particles, unless specified a custom particle
                        NoSound = false, ////Set to false to use vanilla explosion noises, unless specified a custom sound
                        Scale = 1f, //Scale of the particles.
                        CustomParticle = "",
                        CustomSound = "",
                    },
                    Detonation = new DetonateDef //USE THESE MODIFIERS FOR EXPLOSIONS
                    {
                        DetonateOnEnd = true,
                        ArmOnlyOnHit = false,
                        DetonationDamage = 500, //1 Steelplate =100HP, 1 large light armor cube=2500HP. Has a natural falloff based on radius.
                        DetonationRadius = 2, //Measured in meters, how large the explosion should be.
                    },
                    EwarFields = new EwarFieldsDef
                    {
                        Duration = 0,
                        StackDuration = true,
                        Depletable = false,
                        MaxStacks = 10,
                        TriggerRange = 5f,
                    },
                },
                Beams = new BeamDef
                {
                    Enable = false,
                    VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
                    ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
                    RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
                    OneParticle = false, // Only spawn one particle hit per beam weapon.
                },
				Trajectory = new TrajectoryDef
            {
                Guidance = Smart,
                TargetLossDegree = 89,
                TargetLossTime = 600, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                MaxLifeTime = 3600, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                AccelPerSec = 25f,
                DesiredSpeed = 475,
                MaxTrajectory = 11000f,
                FieldTime = 0, // 0 is disabled, a value causes the projectile to come to rest, spawn a field and remain for a time (Measured in game ticks, 60 = 1 second)
                GravityMultiplier = 0f, // Gravity multiplier, influences the trajectory of the projectile, value greater than 0 to enable.
                SpeedVariance = Random(start: 0, end: 0), // subtracts value from DesiredSpeed
                RangeVariance = Random(start: 0, end: 0), // subtracts value from MaxTrajectory
                MaxTrajectoryTime = 0, // How long the weapon must fire before it reaches MaxTrajectory.
                Smarts = new SmartsDef
                {
                    Inaccuracy = 0f, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
                    Aggressiveness = 2f, // controls how responsive tracking is.
                    MaxLateralThrust = .43f, // controls how sharp the trajectile may turn
                    TrackingDelay = 0, // Measured in Shape diameter units traveled.
                    MaxChaseTime = 2400, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    OverideTarget = false, // when set to true ammo picks its own target, does not use hardpoint's.
                    MaxTargets = 3, // Number of targets allowed before ending, 0 = unlimited
                    NoTargetExpire = false, // Expire without ever having a target at TargetLossTime
                                            //OffsetRatio = .30f, // The ratio to offset the random dir (0 to 1) 
                                            //OffsetTime = 120, // how often to offset degree, measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    Roam = false, // Roam current area after target loss
                    KeepAliveAfterTargetLoss = true, // Whether to stop early death of projectile on target loss
                },
                Mines = new MinesDef
                {
                    DetectRadius = 0,
                    DeCloakRadius = 0,
                    FieldTime = 0,
                    Cloak = false,
                    Persist = false,
                },
            },
                AmmoGraphics = new GraphicDef
                {
                    ModelName = "\\Models\\Akiad\\Small\\CharybdisBomb.mwm", //USE DOUBLE LINES, ex: "\\Models\\Name\\Small\\MyGun.mwm"
                    VisualProbability = 1f, //Percentage chance for the model to spawn, "1f" for 100% chance.
                    ShieldHitDraw = true,
                    Particles = new AmmoParticleDef
                    {
                        Ammo = new ParticleDef
                        {
                            Name = "", //Particle that surrounds your projectile. ShipWelderArc
                            Color = Color(red: 1f, green: 1f, blue: 1f, alpha: 1),
                            Offset = Vector(x: 0, y: 0, z: 0),
                            Extras = new ParticleOptionDef
                            {
                                Loop = true,
                                Restart = false,
                                MaxDistance = 5000,
                                MaxDuration = 1,
                                Scale = 0.25f,
                            },
                        },
                        Hit = new ParticleDef
                        {
                            Name = "CycloneExplosion",
                            ApplyToShield = true,
                            ShrinkByDistance = true,
                            Color = Color(red: 1, green: 1, blue: 5, alpha: 1),
                            Offset = Vector(x: 0, y: 0, z: 0),
                            Extras = new ParticleOptionDef
                            {
                                Loop = false,
                                Restart = false,
                                MaxDistance = 500,
                                MaxDuration = 1,
                                Scale = 1f,
                                HitPlayChance = 1f,
                            },
                        },
                    },
                    Lines = new LineDef
                    {
                        TracerMaterial = "ProjectileTrailLine", // Particle that follows your projectile. WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                        ColorVariance = Random(start: 5f, end: 10f), // multiply the color by random values within range.
                        WidthVariance = Random(start: 0f, end: 0.045f), // adds random value to default width (negatives shrinks width)
                        Tracer = new TracerBaseDef
                        {
                            Enable = true,
                            Length = .001f,
                            Width = .001f,
                            Color = Color(red: 0.9f, green: 0.9f, blue: 20f, alpha: 1),
                        },
                        Trail = new TrailDef
                        {
                            Enable = true,
                            Material = "WeaponLaser",
                            DecayTime = 10,
                            Color = Color(red: 20, green: 20, blue: 100, alpha: 1),
                            Back = true,
                            CustomWidth = .05f,
                            UseWidthVariance = false,
                            UseColorFade = true,
                        },
                        OffsetEffect = new OffsetEffectDef
                        {
                            MaxOffset =  0,// 0 offset value disables this effect
                            MinLength = 0.2f,
                            MaxLength = 3,
                        },
                    },
                },
                AmmoAudio = new AmmoAudioDef
                {
                    TravelSound = "",
                    HitSound = "",
                    HitPlayChance = 0.1f,
                    HitPlayShield = true,
                }, // Don't edit below this line

        };
		
		private AmmoDef HEShrapnel => new AmmoDef
        {
            AmmoMagazine = "HEShrapnel",
            AmmoRound = "HEShrapnel",
            HybridRound = false, //AmmoMagazine based weapon with energy cost
            EnergyCost = 0.00000000001f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
            BaseDamage = 1f,
            Mass = 0f, // in kilograms
            Health = 0, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying.
            BackKickForce = 100f,
            DecayPerShot = 0f,
            HardPointUsable = false, // set to false if this is a Fragment ammoType and you don't want the turret to be able to select it directly.
            EnergyMagazineSize = 12, //ONLY USE IF "AmmoMagazine=ENERGY". Otherwise specify magazine size in Ammo.sbc file.
            IgnoreWater = false,

            Shape = new ShapeDef //defines the collision shape of projectile, defaults line and visual Line Length if set to 0
            {
                Shape = LineShape, // LineShape or SphereShape. Do not use SphereShape for fast moving projectiles if you care about precision.
                Diameter = 1, // Diameter is minimum length of LineShape or minimum diameter of SphereShape
            },
            ObjectsHit = new ObjectsHitDef
            {
                MaxObjectsHit = 2, // 0 = disabled
                CountBlocks = true, // counts gridBlocks and not just entities hit
            },
            Fragment = new FragmentDef
            {
                AmmoRound = "",
                Fragments = 0,
                Degrees = 15,
                Reverse = false,
                RandomizeDir = false, // randomize between forward and backward directions
            },
            Pattern = new PatternDef
            {
                Patterns = new[] {
                    "",
                },
                Enable = false,
                TriggerChance = 1f,
                Random = false,
                RandomMin = 1,
                RandomMax = 1,
                SkipParent = false,
                PatternSteps = 1, // Number of Patterns activated per round, will progress in order and loop.  Ignored if Random = true.
            },
            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0f, // 0 = disabled, 1000 = any blocks with currently integrity above 1000 will be immune to damage.
                DamageVoxels = false, // true = voxels are vulnerable to this weapon
                SelfDamage = false, // true = allow self damage.
                HealthHitModifier = 1, // defaults to a value of 1, this setting modifies how much Health is subtracted from a projectile per hit (1 = per hit).
                VoxelHitModifier = 10,
                Characters = 1f,
                // modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01 = 1% damage, 2 = 200% damage.
                FallOff = new FallOffDef
                {
                    Distance = 0f, // Distance at which max damage begins falling off.
                    MinMultipler = 0f, // value from 0.0f to 1f where 0.1f would be a min damage of 10% of max damage.
                },
                Grids = new GridSizeDef
                {
                    Large = -1f,
                    Small = -1f,
                },
                Armor = new ArmorDef
                {
                    Armor = .25f,
                    Light = .25f,
                    Heavy = .1f,
                    NonArmor = 1f,
                },
                Shields = new ShieldDef
                {
                    Modifier = .1f,
                    Type = Default,
                    BypassModifier = -1f,
                },
                // first true/false (ignoreOthers) will cause projectiles to pass through all blocks that do not match the custom subtypeIds.
                Custom = new CustomScalesDef
                {
                    IgnoreAllOthers = false,
                    Types = new[]
                    {
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test1",
                            Modifier = -1f,
                        },
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test2",
                            Modifier = -1f,
                        },
                    },
                },
            },
            AreaEffect = new AreaDamageDef
                {
                    AreaEffect = Explosive, // Disabled = do not use area effect at all, Explosive, Radiant, AntiSmart, JumpNullField, JumpNullField, EnergySinkField, AnchorField, EmpField, OffenseField, NavField, DotField.
                    AreaEffectDamage = 0f, // 0 = DO NOT USE FOR EXPLOSIVES, USE DETONATION SETTINGS INSTEAD
                    AreaEffectRadius = 0f, // DO NOT USE FOR EXPLOSIVES, USE DETONATION SETTINGS INSTEAD
                    Pulse = new PulseDef // interval measured in game ticks (60 == 1 second), pulseChance chance (0 - 100) that an entity in field will be hit
                    {
                        Interval = 30,
                        PulseChance = 0,
                    },
                    Explosions = new ExplosionDef
                    {
                        NoVisuals = false, //Set to false to use vanilla explosion particles, unless specified a custom particle
                        NoSound = false, ////Set to false to use vanilla explosion noises, unless specified a custom sound
                        Scale = .5f, //Scale of the particles.
                        CustomParticle = "",
                        CustomSound = "",
                    },
                    Detonation = new DetonateDef //USE THESE MODIFIERS FOR EXPLOSIONS
                    {
                        DetonateOnEnd = true,
                        ArmOnlyOnHit = false,
                        DetonationDamage = 60, //1 Steelplate =100HP, 1 large light armor cube=2500HP. Has a natural falloff based on radius.
                        DetonationRadius = 2, //Measured in meters, how large the explosion should be.
                    },
                    EwarFields = new EwarFieldsDef
                    {
                        Duration = 0,
                        StackDuration = true,
                        Depletable = false,
                        MaxStacks = 10,
                        TriggerRange = 5f,
                    },
                },
            Beams = new BeamDef
            {
                Enable = false,
                VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
                ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
                RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
                OneParticle = false, // Only spawn one particle hit per beam weapon.
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = None,
                TargetLossDegree = 80f,
                TargetLossTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                MaxLifeTime = 60, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                AccelPerSec = 0f,
                DesiredSpeed = 700, // DO NOT SET HIGHER THAN 4100
                MaxTrajectory = 3660f,
                FieldTime = 0, // 0 is disabled, a value causes the projectile to come to rest, spawn a field and remain for a time (Measured in game ticks, 60 = 1 second)
                GravityMultiplier = 1f, // Gravity multiplier, influences the trajectory of the projectile, value greater than 0 to enable.
                SpeedVariance = Random(start: 0, end: 0), // subtracts value from DesiredSpeed
                RangeVariance = Random(start: 0, end: 0), // subtracts value from MaxTrajectory
                MaxTrajectoryTime = 0, // How long the weapon must fire before it reaches MaxTrajectory.
                Smarts = new SmartsDef
                {
                    Inaccuracy = 5f, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
                    Aggressiveness = 1f, // controls how responsive tracking is.
                    MaxLateralThrust = 0.5, // controls how sharp the trajectile may turn
                    TrackingDelay = 0, // Measured in Shape diameter units traveled.
                    MaxChaseTime = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    OverideTarget = true, // when set to true ammo picks its own target, does not use hardpoint's.
                    MaxTargets = 0, // Number of targets allowed before ending, 0 = unlimited
                    NoTargetExpire = false, // Expire without ever having a target at TargetLossTime
                    Roam = false, // Roam current area after target loss
                    KeepAliveAfterTargetLoss = false, // Whether to stop early death of projectile on target loss
                },
                Mines = new MinesDef
                {
                    DetectRadius = 0,
                    DeCloakRadius = 0,
                    FieldTime = 0,
                    Cloak = false,
                    Persist = false,
                },
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "",
                VisualProbability = 1f,
                ShieldHitDraw = false,
                Particles = new AmmoParticleDef
                {
                    Ammo = new ParticleDef
                    {
                        Name = "", //ShipWelderArc
                        ShrinkByDistance = false,
                        Color = Color(red: 128, green: 0, blue: 0, alpha: 32),
                        Offset = Vector(x: 0, y: -1, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 1,
                            Scale = 1,
                        },
                    },
                    Hit = new ParticleDef
                    {
                        Name = "FireImpact",
                        ApplyToShield = true,
                        ShrinkByDistance = false,
                        Color = Color(red: 3, green: 1.9f, blue: 1f, alpha: 1),
                        Offset = Vector(x: 0, y: 0, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 30,
                            Scale = 1,
                            HitPlayChance = 1f,
                        },
                    },
                    Eject = new ParticleDef
                    {
                        Name = "",
                        ApplyToShield = true,
                        ShrinkByDistance = false,
                        Color = Color(red: 3, green: 1.9f, blue: 1f, alpha: 1),
                        Offset = Vector(x: 0, y: 0, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 30,
                            Scale = 1,
                            HitPlayChance = 1f,
                        },
                    },
                },
                Lines = new LineDef
                {
                    ColorVariance = Random(start: 0.75f, end: 2f), // multiply the color by random values within range.
                    WidthVariance = Random(start: 0f, end: 0f), // adds random value to default width (negatives shrinks width)
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = 1f,
                        Width = 0.2f,
                        Color = Color(red: 3, green: 2, blue: 1f, alpha: 1),
                        VisualFadeStart = 0, // Number of ticks the weapon has been firing before projectiles begin to fade their color
                        VisualFadeEnd = 0, // How many ticks after fade began before it will be invisible.
                        Textures = new[] {// WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                            "ProjectileTrailLine",
                        },
                        TextureMode = Normal, // Normal, Cycle, Chaos, Wave
                        Segmentation = new SegmentDef
                        {
                            Enable = false, // If true Tracer TextureMode is ignored
                            Textures = new[] {
								"",
                            },
                            SegmentLength = 0f, // Uses the values below.
                            SegmentGap = 0f, // Uses Tracer textures and values
                            Speed = 1f, // meters per second
                            Color = Color(red: 1, green: 2, blue: 2.5f, alpha: 1),
                            WidthMultiplier = 1f,
                            Reverse = false,
                            UseLineVariance = true,
                            WidthVariance = Random(start: 0f, end: 0f),
                            ColorVariance = Random(start: 0f, end: 0f)
                        }
                    },
                    Trail = new TrailDef
                    {
                        Enable = false,
                        Textures = new[] {
							"",
                        },
                        TextureMode = Normal,
                        DecayTime = 128,
                        Color = Color(red: 0, green: 0, blue: 1, alpha: 1),
                        Back = false,
                        CustomWidth = 0,
                        UseWidthVariance = false,
                        UseColorFade = true,
                    },
                    OffsetEffect = new OffsetEffectDef
                    {
                        MaxOffset = 0,// 0 offset value disables this effect
                        MinLength = 0.2f,
                        MaxLength = 3,
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                TravelSound = "",
                HitSound = "",
                ShieldHitSound = "",
                PlayerHitSound = "",
                VoxelHitSound = "",
                FloatingHitSound = "",
                HitPlayChance = 0.5f,
                HitPlayShield = true,
            }, // Don't edit below this line
            Ejection = new EjectionDef
            {
                Type = Particle, // Particle or Item (Inventory Component)
                Speed = 100f, // Speed inventory is ejected from in dummy direction
                SpawnChance = 0.5f, // chance of triggering effect (0 - 1)
                CompDef = new ComponentDef
                {
                    ItemName = "", //InventoryComponent name
                    ItemLifeTime = 0, // how long item should exist in world
                    Delay = 0, // delay in ticks after shot before ejected
                }
            },
        };
		
		private AmmoDef FlakShrapnel => new AmmoDef
        {
            AmmoMagazine = "FlakShrapnel",
            AmmoRound = "FlakShrapnel",
            HybridRound = false, //AmmoMagazine based weapon with energy cost
            EnergyCost = 0.00000000001f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
            BaseDamage = 60f,
            Mass = 0f, // in kilograms
            Health = 0, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying.
            BackKickForce = 100f,
            DecayPerShot = 0f,
            HardPointUsable = false, // set to false if this is a Fragment ammoType and you don't want the turret to be able to select it directly.
            EnergyMagazineSize = 12, //ONLY USE IF "AmmoMagazine=ENERGY". Otherwise specify magazine size in Ammo.sbc file.
            IgnoreWater = false,

            Shape = new ShapeDef //defines the collision shape of projectile, defaults line and visual Line Length if set to 0
            {
                Shape = LineShape, // LineShape or SphereShape. Do not use SphereShape for fast moving projectiles if you care about precision.
                Diameter = 1, // Diameter is minimum length of LineShape or minimum diameter of SphereShape
            },
            ObjectsHit = new ObjectsHitDef
            {
                MaxObjectsHit = 2, // 0 = disabled
                CountBlocks = true, // counts gridBlocks and not just entities hit
            },
            Fragment = new FragmentDef
            {
                AmmoRound = "",
                Fragments = 0,
                Degrees = 15,
                Reverse = false,
                RandomizeDir = false, // randomize between forward and backward directions
            },
            Pattern = new PatternDef
            {
                Patterns = new[] {
                    "",
                },
                Enable = false,
                TriggerChance = 1f,
                Random = false,
                RandomMin = 1,
                RandomMax = 1,
                SkipParent = false,
                PatternSteps = 1, // Number of Patterns activated per round, will progress in order and loop.  Ignored if Random = true.
            },
            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0f, // 0 = disabled, 1000 = any blocks with currently integrity above 1000 will be immune to damage.
                DamageVoxels = false, // true = voxels are vulnerable to this weapon
                SelfDamage = false, // true = allow self damage.
                HealthHitModifier = 1, // defaults to a value of 1, this setting modifies how much Health is subtracted from a projectile per hit (1 = per hit).
                VoxelHitModifier = 10,
                Characters = 1f,
                // modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01 = 1% damage, 2 = 200% damage.
                FallOff = new FallOffDef
                {
                    Distance = 0f, // Distance at which max damage begins falling off.
                    MinMultipler = 0f, // value from 0.0f to 1f where 0.1f would be a min damage of 10% of max damage.
                },
                Grids = new GridSizeDef
                {
                    Large = -1f,
                    Small = -1f,
                },
                Armor = new ArmorDef
                {
                    Armor = .25f,
                    Light = .25f,
                    Heavy = .1f,
                    NonArmor = 1f,
                },
                Shields = new ShieldDef
                {
                    Modifier = .1f,
                    Type = Default,
                    BypassModifier = -1f,
                },
                // first true/false (ignoreOthers) will cause projectiles to pass through all blocks that do not match the custom subtypeIds.
                Custom = new CustomScalesDef
                {
                    IgnoreAllOthers = false,
                    Types = new[]
                    {
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test1",
                            Modifier = -1f,
                        },
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test2",
                            Modifier = -1f,
                        },
                    },
                },
            },
            AreaEffect = new AreaDamageDef
                {
                    AreaEffect = Explosive, // Disabled = do not use area effect at all, Explosive, Radiant, AntiSmart, JumpNullField, JumpNullField, EnergySinkField, AnchorField, EmpField, OffenseField, NavField, DotField.
                    AreaEffectDamage = 0f, // 0 = DO NOT USE FOR EXPLOSIVES, USE DETONATION SETTINGS INSTEAD
                    AreaEffectRadius = 0f, // DO NOT USE FOR EXPLOSIVES, USE DETONATION SETTINGS INSTEAD
                    Pulse = new PulseDef // interval measured in game ticks (60 == 1 second), pulseChance chance (0 - 100) that an entity in field will be hit
                    {
                        Interval = 30,
                        PulseChance = 0,
                    },
                    Explosions = new ExplosionDef
                    {
                        NoVisuals = false, //Set to false to use vanilla explosion particles, unless specified a custom particle
                        NoSound = false, ////Set to false to use vanilla explosion noises, unless specified a custom sound
                        Scale = .5f, //Scale of the particles.
                        CustomParticle = "",
                        CustomSound = "",
                    },
                    Detonation = new DetonateDef //USE THESE MODIFIERS FOR EXPLOSIONS
                    {
                        DetonateOnEnd = true,
                        ArmOnlyOnHit = false,
                        DetonationDamage = 60, //1 Steelplate =100HP, 1 large light armor cube=2500HP. Has a natural falloff based on radius.
                        DetonationRadius = 0, //Measured in meters, how large the explosion should be.
                    },
                    EwarFields = new EwarFieldsDef
                    {
                        Duration = 0,
                        StackDuration = true,
                        Depletable = false,
                        MaxStacks = 10,
                        TriggerRange = 5f,
                    },
                },
            Beams = new BeamDef
            {
                Enable = false,
                VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
                ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
                RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
                OneParticle = false, // Only spawn one particle hit per beam weapon.
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = None,
                TargetLossDegree = 80f,
                TargetLossTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                MaxLifeTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                AccelPerSec = 0f,
                DesiredSpeed = 800, // DO NOT SET HIGHER THAN 4100
                MaxTrajectory = 50f,
                FieldTime = 0, // 0 is disabled, a value causes the projectile to come to rest, spawn a field and remain for a time (Measured in game ticks, 60 = 1 second)
                GravityMultiplier = 1f, // Gravity multiplier, influences the trajectory of the projectile, value greater than 0 to enable.
                SpeedVariance = Random(start: 0, end: 0), // subtracts value from DesiredSpeed
                RangeVariance = Random(start: 0, end: 0), // subtracts value from MaxTrajectory
                MaxTrajectoryTime = 0, // How long the weapon must fire before it reaches MaxTrajectory.
                Smarts = new SmartsDef
                {
                    Inaccuracy = 5f, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
                    Aggressiveness = 1f, // controls how responsive tracking is.
                    MaxLateralThrust = 0.5, // controls how sharp the trajectile may turn
                    TrackingDelay = 0, // Measured in Shape diameter units traveled.
                    MaxChaseTime = 0, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    OverideTarget = true, // when set to true ammo picks its own target, does not use hardpoint's.
                    MaxTargets = 0, // Number of targets allowed before ending, 0 = unlimited
                    NoTargetExpire = false, // Expire without ever having a target at TargetLossTime
                    Roam = false, // Roam current area after target loss
                    KeepAliveAfterTargetLoss = false, // Whether to stop early death of projectile on target loss
                },
                Mines = new MinesDef
                {
                    DetectRadius = 0,
                    DeCloakRadius = 0,
                    FieldTime = 0,
                    Cloak = false,
                    Persist = false,
                },
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "",
                VisualProbability = 1f,
                ShieldHitDraw = false,
                Particles = new AmmoParticleDef
                {
                    Ammo = new ParticleDef
                    {
                        Name = "", //ShipWelderArc
                        ShrinkByDistance = false,
                        Color = Color(red: 128, green: 0, blue: 0, alpha: 32),
                        Offset = Vector(x: 0, y: -1, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 1,
                            Scale = 1,
                        },
                    },
                    Hit = new ParticleDef
                    {
                        Name = "",
                        ApplyToShield = true,
                        ShrinkByDistance = false,
                        Color = Color(red: 3, green: 1.9f, blue: 1f, alpha: 1),
                        Offset = Vector(x: 0, y: 0, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 30,
                            Scale = 1,
                            HitPlayChance = 1f,
                        },
                    },
                    Eject = new ParticleDef
                    {
                        Name = "",
                        ApplyToShield = true,
                        ShrinkByDistance = false,
                        Color = Color(red: 3, green: 1.9f, blue: 1f, alpha: 1),
                        Offset = Vector(x: 0, y: 0, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 30,
                            Scale = 1,
                            HitPlayChance = 1f,
                        },
                    },
                },
                Lines = new LineDef
                {
                    ColorVariance = Random(start: 0.75f, end: 2f), // multiply the color by random values within range.
                    WidthVariance = Random(start: 0f, end: 0f), // adds random value to default width (negatives shrinks width)
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = 1f,
                        Width = 0.2f,
                        Color = Color(red: 3, green: 2, blue: 1f, alpha: 1),
                        VisualFadeStart = 0, // Number of ticks the weapon has been firing before projectiles begin to fade their color
                        VisualFadeEnd = 0, // How many ticks after fade began before it will be invisible.
                        Textures = new[] {// WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                            "ProjectileTrailLine",
                        },
                        TextureMode = Normal, // Normal, Cycle, Chaos, Wave
                        Segmentation = new SegmentDef
                        {
                            Enable = false, // If true Tracer TextureMode is ignored
                            Textures = new[] {
								"",
                            },
                            SegmentLength = 0f, // Uses the values below.
                            SegmentGap = 0f, // Uses Tracer textures and values
                            Speed = 1f, // meters per second
                            Color = Color(red: 1, green: 2, blue: 2.5f, alpha: 1),
                            WidthMultiplier = 1f,
                            Reverse = false,
                            UseLineVariance = true,
                            WidthVariance = Random(start: 0f, end: 0f),
                            ColorVariance = Random(start: 0f, end: 0f)
                        }
                    },
                    Trail = new TrailDef
                    {
                        Enable = false,
                        Textures = new[] {
							"",
                        },
                        TextureMode = Normal,
                        DecayTime = 128,
                        Color = Color(red: 0, green: 0, blue: 1, alpha: 1),
                        Back = false,
                        CustomWidth = 0,
                        UseWidthVariance = false,
                        UseColorFade = true,
                    },
                    OffsetEffect = new OffsetEffectDef
                    {
                        MaxOffset = 0,// 0 offset value disables this effect
                        MinLength = 0.2f,
                        MaxLength = 3,
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                TravelSound = "",
                HitSound = "",
                ShieldHitSound = "",
                PlayerHitSound = "",
                VoxelHitSound = "",
                FloatingHitSound = "",
                HitPlayChance = 0.5f,
                HitPlayShield = true,
            }, // Don't edit below this line
            Ejection = new EjectionDef
            {
                Type = Particle, // Particle or Item (Inventory Component)
                Speed = 100f, // Speed inventory is ejected from in dummy direction
                SpawnChance = 0.5f, // chance of triggering effect (0 - 1)
                CompDef = new ComponentDef
                {
                    ItemName = "", //InventoryComponent name
                    ItemLifeTime = 0, // how long item should exist in world
                    Delay = 0, // delay in ticks after shot before ejected
                }
            },
        };
		
		private AmmoDef ChimeraFire1 => new AmmoDef
        {
            AmmoMagazine = "ChimeraTank",
            AmmoRound = "ChimeraFire1",
            HybridRound = false, //AmmoMagazine based weapon with energy cost
            EnergyCost = 0.00000000001f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
            BaseDamage = 10f,
            Mass = 0f, // in kilograms
            Health = 0, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying.
            BackKickForce = 10f,
            DecayPerShot = 0f,
            HardPointUsable = true, // set to false if this is a Fragment ammoType and you don't want the turret to be able to select it directly.
            EnergyMagazineSize = 12, //ONLY USE IF "AmmoMagazine=ENERGY". Otherwise specify magazine size in Ammo.sbc file.
            IgnoreWater = false,

            Shape = new ShapeDef //defines the collision shape of projectile, defaults line and visual Line Length if set to 0
                {
                    Shape = LineShape,
                    Diameter = 1,
                },
                ObjectsHit = new ObjectsHitDef
                {
                    MaxObjectsHit = 0, // 0 = disabled
                    CountBlocks = false, // counts gridBlocks and not just entities hit
                },
                Fragment = new FragmentDef
				{
					AmmoRound = "ChimeraFire2",
					Fragments = 1, //Number of Fragment projectiles created on impact.
					Degrees = 1,  //Think of this as a cone in front of the projectile, 360 degrees for a full circle.
					Reverse = false, //Redirects the cone back towards the projectile's rear
					RandomizeDir = false,
				},
                DamageScales = new DamageScaleDef
                {
                    MaxIntegrity = 0f, // 0 = disabled, 1000 = any blocks with currently integrity above 1000 will be immune to damage.
                    DamageVoxels = false, // true = voxels are vulnerable to this weapon
                    SelfDamage = false, // true = allow self damage.
                    HealthHitModifier = 1, // defaults to a value of 1, this setting modifies how much Health is subtracted from a projectile per hit (1 = per hit).
                    VoxelHitModifier = 3,  // modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01 = 1% damage, 2 = 200% damage.
                    Characters = 0.2f, //Damage to characters. 1f=same as damage to grids
                    Grids = new GridSizeDef
                    {
                        Large = -1f,
                        Small = .5f,
                    },
                    Armor = new ArmorDef
                    {
                        Armor = .25f,
                        Light = -1f,
                        Heavy = -1f,
                        NonArmor = 1f,
                    },
                    Shields = new ShieldDef
                    {
                        Modifier = .5f,
                        Type = Default,
                        BypassModifier = -1f,
                    },
                    // first true/false (ignoreOthers) will cause projectiles to pass through all blocks that do not match the custom subtypeIds.
                    Custom = new CustomScalesDef
                    {
                        IgnoreAllOthers = false,
                        Types = new []
                        {
                            new CustomBlocksDef
                            {
                                SubTypeId = "Test1",
                                Modifier = -1f,
                            },
                            new CustomBlocksDef
                            {
                                SubTypeId = "Test2",
                                Modifier = -1f,
                            },
                        },
                    },
                },
                AreaEffect = new AreaDamageDef
                {
                    AreaEffect = Radiant, // Disabled = do not use area effect at all, Explosive, Radiant, AntiSmart, JumpNullField, JumpNullField, EnergySinkField, AnchorField, EmpField, OffenseField, NavField, DotField.
                    AreaEffectDamage = 30f, // 0 = DO NOT USE FOR EXPLOSIVES, USE DETONATION SETTINGS INSTEAD
                    AreaEffectRadius = 5f, // DO NOT USE FOR EXPLOSIVES, USE DETONATION SETTINGS INSTEAD
                    Pulse = new PulseDef // interval measured in game ticks (60 == 1 second), pulseChance chance (0 - 100) that an entity in field will be hit
                    {
                        Interval = 30,
                        PulseChance = 0,
                    },
                    Explosions = new ExplosionDef
                    {
                        NoVisuals = false, //Set to false to use vanilla explosion particles, unless specified a custom particle
                        NoSound = false, ////Set to false to use vanilla explosion noises, unless specified a custom sound
                        Scale = 2, //Scale of the particles.
                        CustomParticle = "",
                        CustomSound = "",
                    },
                    Detonation = new DetonateDef //USE THESE MODIFIERS FOR EXPLOSIONS
                    {
                        DetonateOnEnd = true,
                        ArmOnlyOnHit = false,
                        DetonationDamage = 0, //1 Steelplate =100HP, 1 large light armor cube=2500HP. Has a natural falloff based on radius.
                        DetonationRadius = 5, //Measured in meters, how large the explosion should be.
                    },
                    EwarFields = new EwarFieldsDef
                    {
                        Duration = 0,
                        StackDuration = true,
                        Depletable = false,
                        MaxStacks = 10,
                        TriggerRange = 5f,
                    },
                },
                Beams = new BeamDef
                {
                    Enable = false,
                    VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
                    ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
                    RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
                    OneParticle = false, // Only spawn one particle hit per beam weapon.
                },
                Trajectory = new TrajectoryDef
                {
                    Guidance = None,
                    TargetLossDegree = 180f,
                    TargetLossTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    MaxLifeTime = 75, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    AccelPerSec = 0f,
                    DesiredSpeed = 40, //Speed of the projectile. DO NOT SET HIGHER THAN 4100.
                    MaxTrajectory = 200f, //Maximum distance the projectile can travel before detonating/despawning.
                    FieldTime = 0, // 0 is disabled, a value causes the projectile to come to rest, spawn a field and remain for a time (Measured in game ticks, 60 = 1 second)
					GravityMultiplier = 1f, //1f=Gravity applies normal pull to projectile. 0 to disable.
                    SpeedVariance = Random(start: 0, end: 0), // subtracts value from DesiredSpeed
                    RangeVariance = Random(start: 0, end: 0), // subtracts value from MaxTrajectory
                    Smarts = new SmartsDef
                    {
                        Inaccuracy = 5f, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
                        Aggressiveness = 0.1f, // controls how responsive tracking is.
                        MaxLateralThrust = 0.05f, // controls how sharp the trajectile may turn
                        TrackingDelay = 400, // Measured in Shape diameter units traveled.
                        MaxChaseTime = 2000, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                        OverideTarget = true, // when set to true ammo picks its own target, does not use hardpoint's.
                    },
                    Mines = new MinesDef
                    {
                        DetectRadius =  200,
                        DeCloakRadius = 100,
                        FieldTime = 1800,
                        Cloak = true,
                        Persist = false,
                    },
                },
                AmmoGraphics = new GraphicDef
                {
                    ModelName = "", //USE DOUBLE LINES, ex: "\\Models\\Name\\Small\\MyGun.mwm"
                    VisualProbability = 1f, //Percentage chance for the model to spawn, "1f" for 100% chance.
                    ShieldHitDraw = true,
                    Particles = new AmmoParticleDef
                    {
                        Ammo = new ParticleDef
                        {
                            Name = "Chimera1", //Particle that surrounds your projectile. ShipWelderArc
                            Color = Color(red: 1f, green: 1f, blue: 1f, alpha: 1),
                            Offset = Vector(x: 0, y: 0, z: 0),
                            Extras = new ParticleOptionDef
                            {
                                Loop = true,
                                Restart = false,
                                MaxDistance = 5000,
                                MaxDuration = 3,
                                Scale = .6f,
                            },
                        },
                        Hit = new ParticleDef
                        {
                            Name = "ChimeraFire",
                            ApplyToShield = true,
                            ShrinkByDistance = true,
                            Color = Color(red: 3, green: 2, blue: 1, alpha: 1),
                            Offset = Vector(x: 0, y: 0, z: 0),
                            Extras = new ParticleOptionDef
                            {
                                Loop = false,
                                Restart = false,
                                MaxDistance = 500,
                                MaxDuration = 1,
                                Scale = 1,
                                HitPlayChance = 1f,
                            },
                        },
                    },
                    Lines = new LineDef
                    {
                        TracerMaterial = "ProjectileTrailLine", // Particle that follows your projectile. WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                        ColorVariance = Random(start: 5f, end: 10f), // multiply the color by random values within range.
                        WidthVariance = Random(start: 0f, end: 0.045f), // adds random value to default width (negatives shrinks width)
                        Tracer = new TracerBaseDef
                        {
                            Enable = true,
                            Length = 2f,
                            Width = 0.02f,
                            Color = Color(red: 100f, green: 50f, blue: 50f, alpha: 25),
                        },
                        Trail = new TrailDef
                        {
                            Enable = false,
                            Material = "WeaponLaser",
                            DecayTime = 128,
                            Color = Color(red: 0, green: 0, blue: 1, alpha: 1),
                            Back = true,
                            CustomWidth = 0,
                            UseWidthVariance = false,
                            UseColorFade = true,
                        },
                        OffsetEffect = new OffsetEffectDef
                        {
                            MaxOffset =  0,// 0 offset value disables this effect
                            MinLength = 0.2f,
                            MaxLength = 3,
                        },
                    },
                },
                AmmoAudio = new AmmoAudioDef
                {
                    TravelSound = "ChimeraFire",
                    HitSound = "",
                    HitPlayChance = 0.1f,
                    HitPlayShield = true,
                }, // Don't edit below this line

        };
		private AmmoDef ChimeraFire2 => new AmmoDef
        {
            AmmoMagazine = "ChimeraTank",
            AmmoRound = "ChimeraFire2",
            HybridRound = false, //AmmoMagazine based weapon with energy cost
            EnergyCost = 0.00000000001f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
            BaseDamage = 10f,
            Mass = 0f, // in kilograms
            Health = 0, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying.
            BackKickForce = 10f,
            DecayPerShot = 0f,
            HardPointUsable = false, // set to false if this is a Fragment ammoType and you don't want the turret to be able to select it directly.
            EnergyMagazineSize = 12, //ONLY USE IF "AmmoMagazine=ENERGY". Otherwise specify magazine size in Ammo.sbc file.
            IgnoreWater = false,

            Shape = new ShapeDef //defines the collision shape of projectile, defaults line and visual Line Length if set to 0
                {
                    Shape = LineShape,
                    Diameter = 1,
                },
                ObjectsHit = new ObjectsHitDef
                {
                    MaxObjectsHit = 0, // 0 = disabled
                    CountBlocks = false, // counts gridBlocks and not just entities hit
                },
                Fragment = new FragmentDef
				{
					AmmoRound = "ChimeraFire3",
					Fragments = 1, //Number of Fragment projectiles created on impact.
					Degrees = 1,  //Think of this as a cone in front of the projectile, 360 degrees for a full circle.
					Reverse = false, //Redirects the cone back towards the projectile's rear
					RandomizeDir = false,
				},
                DamageScales = new DamageScaleDef
                {
                    MaxIntegrity = 0f, // 0 = disabled, 1000 = any blocks with currently integrity above 1000 will be immune to damage.
                    DamageVoxels = false, // true = voxels are vulnerable to this weapon
                    SelfDamage = false, // true = allow self damage.
                    HealthHitModifier = 1, // defaults to a value of 1, this setting modifies how much Health is subtracted from a projectile per hit (1 = per hit).
                    VoxelHitModifier = 3,  // modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01 = 1% damage, 2 = 200% damage.
                    Characters = 0.2f, //Damage to characters. 1f=same as damage to grids
                    Grids = new GridSizeDef
                    {
                        Large = -1f,
                        Small = .5f,
                    },
                    Armor = new ArmorDef
                    {
                        Armor = .25f,
                        Light = -1f,
                        Heavy = -1f,
                        NonArmor = 1f,
                    },
                    Shields = new ShieldDef
                    {
                        Modifier = .5f,
                        Type = Default,
                        BypassModifier = -1f,
                    },
                    // first true/false (ignoreOthers) will cause projectiles to pass through all blocks that do not match the custom subtypeIds.
                    Custom = new CustomScalesDef
                    {
                        IgnoreAllOthers = false,
                        Types = new []
                        {
                            new CustomBlocksDef
                            {
                                SubTypeId = "Test1",
                                Modifier = -1f,
                            },
                            new CustomBlocksDef
                            {
                                SubTypeId = "Test2",
                                Modifier = -1f,
                            },
                        },
                    },
                },
                AreaEffect = new AreaDamageDef
                {
                    AreaEffect = Radiant, // Disabled = do not use area effect at all, Explosive, Radiant, AntiSmart, JumpNullField, JumpNullField, EnergySinkField, AnchorField, EmpField, OffenseField, NavField, DotField.
                    AreaEffectDamage = 30f, // 0 = DO NOT USE FOR EXPLOSIVES, USE DETONATION SETTINGS INSTEAD
                    AreaEffectRadius = 5f, // DO NOT USE FOR EXPLOSIVES, USE DETONATION SETTINGS INSTEAD
                    Pulse = new PulseDef // interval measured in game ticks (60 == 1 second), pulseChance chance (0 - 100) that an entity in field will be hit
                    {
                        Interval = 30,
                        PulseChance = 0,
                    },
                    Explosions = new ExplosionDef
                    {
                        NoVisuals = false, //Set to false to use vanilla explosion particles, unless specified a custom particle
                        NoSound = false, ////Set to false to use vanilla explosion noises, unless specified a custom sound
                        Scale = 2, //Scale of the particles.
                        CustomParticle = "",
                        CustomSound = "",
                    },
                    Detonation = new DetonateDef //USE THESE MODIFIERS FOR EXPLOSIONS
                    {
                        DetonateOnEnd = true,
                        ArmOnlyOnHit = false,
                        DetonationDamage = 0, //1 Steelplate =100HP, 1 large light armor cube=2500HP. Has a natural falloff based on radius.
                        DetonationRadius = 5, //Measured in meters, how large the explosion should be.
                    },
                    EwarFields = new EwarFieldsDef
                    {
                        Duration = 0,
                        StackDuration = true,
                        Depletable = false,
                        MaxStacks = 10,
                        TriggerRange = 5f,
                    },
                },
                Beams = new BeamDef
                {
                    Enable = false,
                    VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
                    ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
                    RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
                    OneParticle = false, // Only spawn one particle hit per beam weapon.
                },
                Trajectory = new TrajectoryDef
                {
                    Guidance = None,
                    TargetLossDegree = 180f,
                    TargetLossTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    MaxLifeTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    AccelPerSec = 0f,
                    DesiredSpeed = 40, //Speed of the projectile. DO NOT SET HIGHER THAN 4100.
                    MaxTrajectory = 40f, //Maximum distance the projectile can travel before detonating/despawning.
                    FieldTime = 0, // 0 is disabled, a value causes the projectile to come to rest, spawn a field and remain for a time (Measured in game ticks, 60 = 1 second)
					GravityMultiplier = 1f, //1f=Gravity applies normal pull to projectile. 0 to disable.
                    SpeedVariance = Random(start: 0, end: 0), // subtracts value from DesiredSpeed
                    RangeVariance = Random(start: 0, end: 0), // subtracts value from MaxTrajectory
                    Smarts = new SmartsDef
                    {
                        Inaccuracy = 5f, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
                        Aggressiveness = 0.1f, // controls how responsive tracking is.
                        MaxLateralThrust = 0.05f, // controls how sharp the trajectile may turn
                        TrackingDelay = 400, // Measured in Shape diameter units traveled.
                        MaxChaseTime = 2000, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                        OverideTarget = true, // when set to true ammo picks its own target, does not use hardpoint's.
                    },
                    Mines = new MinesDef
                    {
                        DetectRadius =  200,
                        DeCloakRadius = 100,
                        FieldTime = 1800,
                        Cloak = true,
                        Persist = false,
                    },
                },
                AmmoGraphics = new GraphicDef
                {
                    ModelName = "", //USE DOUBLE LINES, ex: "\\Models\\Name\\Small\\MyGun.mwm"
                    VisualProbability = 1f, //Percentage chance for the model to spawn, "1f" for 100% chance.
                    ShieldHitDraw = true,
                    Particles = new AmmoParticleDef
                    {
                        Ammo = new ParticleDef
                        {
                            Name = "Chimera2", //Particle that surrounds your projectile. ShipWelderArc
                            Color = Color(red: 1f, green: 1f, blue: 1f, alpha: 1),
                            Offset = Vector(x: 0, y: 0, z: -10),
                            Extras = new ParticleOptionDef
                            {
                                Loop = true,
                                Restart = false,
                                MaxDistance = 5000,
                                MaxDuration = 3,
                                Scale = .7f,
                            },
                        },
                        Hit = new ParticleDef
                        {
                            Name = "ChimeraFire",
                            ApplyToShield = true,
                            ShrinkByDistance = true,
                            Color = Color(red: 3, green: 2, blue: 1, alpha: 1),
                            Offset = Vector(x: 0, y: 0, z: 0),
                            Extras = new ParticleOptionDef
                            {
                                Loop = false,
                                Restart = false,
                                MaxDistance = 500,
                                MaxDuration = 1,
                                Scale = 1,
                                HitPlayChance = 1f,
                            },
                        },
                    },
                    Lines = new LineDef
                    {
                        TracerMaterial = "ProjectileTrailLine", // Particle that follows your projectile. WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                        ColorVariance = Random(start: 5f, end: 10f), // multiply the color by random values within range.
                        WidthVariance = Random(start: 0f, end: 0.045f), // adds random value to default width (negatives shrinks width)
                        Tracer = new TracerBaseDef
                        {
                            Enable = false,
                            Length = 2f,
                            Width = 0.02f,
                            Color = Color(red: 100f, green: 50f, blue: 50f, alpha: 25),
                        },
                        Trail = new TrailDef
                        {
                            Enable = false,
                            Material = "WeaponLaser",
                            DecayTime = 128,
                            Color = Color(red: 0, green: 0, blue: 1, alpha: 1),
                            Back = true,
                            CustomWidth = 0,
                            UseWidthVariance = false,
                            UseColorFade = true,
                        },
                        OffsetEffect = new OffsetEffectDef
                        {
                            MaxOffset =  0,// 0 offset value disables this effect
                            MinLength = 0.2f,
                            MaxLength = 3,
                        },
                    },
                },
                AmmoAudio = new AmmoAudioDef
                {
                    TravelSound = "ChimeraFire",
                    HitSound = "",
                    HitPlayChance = 0.1f,
                    HitPlayShield = true,
                }, // Don't edit below this line

        };
		private AmmoDef ChimeraFire3 => new AmmoDef
        {
            AmmoMagazine = "ChimeraTank",
            AmmoRound = "ChimeraFire3",
            HybridRound = false, //AmmoMagazine based weapon with energy cost
            EnergyCost = 0.00000000001f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
            BaseDamage = 10f,
            Mass = 0f, // in kilograms
            Health = 0, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying.
            BackKickForce = 10f,
            DecayPerShot = 0f,
            HardPointUsable = false, // set to false if this is a Fragment ammoType and you don't want the turret to be able to select it directly.
            EnergyMagazineSize = 12, //ONLY USE IF "AmmoMagazine=ENERGY". Otherwise specify magazine size in Ammo.sbc file.
            IgnoreWater = false,

            Shape = new ShapeDef //defines the collision shape of projectile, defaults line and visual Line Length if set to 0
                {
                    Shape = LineShape,
                    Diameter = 1,
                },
                ObjectsHit = new ObjectsHitDef
                {
                    MaxObjectsHit = 0, // 0 = disabled
                    CountBlocks = false, // counts gridBlocks and not just entities hit
                },
                Fragment = new FragmentDef
				{
					AmmoRound = "ChimeraFire4",
					Fragments = 1, //Number of Fragment projectiles created on impact.
					Degrees = 1,  //Think of this as a cone in front of the projectile, 360 degrees for a full circle.
					Reverse = false, //Redirects the cone back towards the projectile's rear
					RandomizeDir = false,
				},
                DamageScales = new DamageScaleDef
                {
                    MaxIntegrity = 0f, // 0 = disabled, 1000 = any blocks with currently integrity above 1000 will be immune to damage.
                    DamageVoxels = false, // true = voxels are vulnerable to this weapon
                    SelfDamage = false, // true = allow self damage.
                    HealthHitModifier = 1, // defaults to a value of 1, this setting modifies how much Health is subtracted from a projectile per hit (1 = per hit).
                    VoxelHitModifier = 3,  // modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01 = 1% damage, 2 = 200% damage.
                    Characters = 0.2f, //Damage to characters. 1f=same as damage to grids
                    Grids = new GridSizeDef
                    {
                        Large = -1f,
                        Small = .5f,
                    },
                    Armor = new ArmorDef
                    {
                        Armor = .25f,
                        Light = -1f,
                        Heavy = -1f,
                        NonArmor = 1f,
                    },
                    Shields = new ShieldDef
                    {
                        Modifier = .5f,
                        Type = Default,
                        BypassModifier = -1f,
                    },
                    // first true/false (ignoreOthers) will cause projectiles to pass through all blocks that do not match the custom subtypeIds.
                    Custom = new CustomScalesDef
                    {
                        IgnoreAllOthers = false,
                        Types = new []
                        {
                            new CustomBlocksDef
                            {
                                SubTypeId = "Test1",
                                Modifier = -1f,
                            },
                            new CustomBlocksDef
                            {
                                SubTypeId = "Test2",
                                Modifier = -1f,
                            },
                        },
                    },
                },
                AreaEffect = new AreaDamageDef
                {
                    AreaEffect = Radiant, // Disabled = do not use area effect at all, Explosive, Radiant, AntiSmart, JumpNullField, JumpNullField, EnergySinkField, AnchorField, EmpField, OffenseField, NavField, DotField.
                    AreaEffectDamage = 30f, // 0 = DO NOT USE FOR EXPLOSIVES, USE DETONATION SETTINGS INSTEAD
                    AreaEffectRadius = 5f, // DO NOT USE FOR EXPLOSIVES, USE DETONATION SETTINGS INSTEAD
                    Pulse = new PulseDef // interval measured in game ticks (60 == 1 second), pulseChance chance (0 - 100) that an entity in field will be hit
                    {
                        Interval = 30,
                        PulseChance = 0,
                    },
                    Explosions = new ExplosionDef
                    {
                        NoVisuals = false, //Set to false to use vanilla explosion particles, unless specified a custom particle
                        NoSound = false, ////Set to false to use vanilla explosion noises, unless specified a custom sound
                        Scale = 2, //Scale of the particles.
                        CustomParticle = "",
                        CustomSound = "",
                    },
                    Detonation = new DetonateDef //USE THESE MODIFIERS FOR EXPLOSIONS
                    {
                        DetonateOnEnd = true,
                        ArmOnlyOnHit = false,
                        DetonationDamage = 0, //1 Steelplate =100HP, 1 large light armor cube=2500HP. Has a natural falloff based on radius.
                        DetonationRadius = 5, //Measured in meters, how large the explosion should be.
                    },
                    EwarFields = new EwarFieldsDef
                    {
                        Duration = 0,
                        StackDuration = true,
                        Depletable = false,
                        MaxStacks = 10,
                        TriggerRange = 5f,
                    },
                },
                Beams = new BeamDef
                {
                    Enable = false,
                    VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
                    ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
                    RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
                    OneParticle = false, // Only spawn one particle hit per beam weapon.
                },
                Trajectory = new TrajectoryDef
                {
                    Guidance = None,
                    TargetLossDegree = 180f,
                    TargetLossTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    MaxLifeTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    AccelPerSec = 0f,
                    DesiredSpeed = 40, //Speed of the projectile. DO NOT SET HIGHER THAN 4100.
                    MaxTrajectory = 40f, //Maximum distance the projectile can travel before detonating/despawning.
                    FieldTime = 0, // 0 is disabled, a value causes the projectile to come to rest, spawn a field and remain for a time (Measured in game ticks, 60 = 1 second)
					GravityMultiplier = 1f, //1f=Gravity applies normal pull to projectile. 0 to disable.
                    SpeedVariance = Random(start: 0, end: 0), // subtracts value from DesiredSpeed
                    RangeVariance = Random(start: 0, end: 0), // subtracts value from MaxTrajectory
                    Smarts = new SmartsDef
                    {
                        Inaccuracy = 5f, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
                        Aggressiveness = 0.1f, // controls how responsive tracking is.
                        MaxLateralThrust = 0.05f, // controls how sharp the trajectile may turn
                        TrackingDelay = 400, // Measured in Shape diameter units traveled.
                        MaxChaseTime = 2000, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                        OverideTarget = true, // when set to true ammo picks its own target, does not use hardpoint's.
                    },
                    Mines = new MinesDef
                    {
                        DetectRadius =  200,
                        DeCloakRadius = 100,
                        FieldTime = 1800,
                        Cloak = true,
                        Persist = false,
                    },
                },
                AmmoGraphics = new GraphicDef
                {
                    ModelName = "", //USE DOUBLE LINES, ex: "\\Models\\Name\\Small\\MyGun.mwm"
                    VisualProbability = 1f, //Percentage chance for the model to spawn, "1f" for 100% chance.
                    ShieldHitDraw = true,
                    Particles = new AmmoParticleDef
                    {
                        Ammo = new ParticleDef
                        {
                            Name = "Chimera3", //Particle that surrounds your projectile. ShipWelderArc
                            Color = Color(red: 1f, green: 1f, blue: 1f, alpha: 1),
                            Offset = Vector(x: 0, y: 0, z: 0),
                            Extras = new ParticleOptionDef
                            {
                                Loop = true,
                                Restart = false,
                                MaxDistance = 5000,
                                MaxDuration = 3,
                                Scale = .8f,
                            },
                        },
                        Hit = new ParticleDef
                        {
                            Name = "ChimeraFire",
                            ApplyToShield = true,
                            ShrinkByDistance = true,
                            Color = Color(red: 3, green: 2, blue: 1, alpha: 1),
                            Offset = Vector(x: 0, y: 0, z: 0),
                            Extras = new ParticleOptionDef
                            {
                                Loop = false,
                                Restart = false,
                                MaxDistance = 500,
                                MaxDuration = 1,
                                Scale = 1,
                                HitPlayChance = 1f,
                            },
                        },
                    },
                    Lines = new LineDef
                    {
                        TracerMaterial = "ProjectileTrailLine", // Particle that follows your projectile. WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                        ColorVariance = Random(start: 5f, end: 10f), // multiply the color by random values within range.
                        WidthVariance = Random(start: 0f, end: 0.045f), // adds random value to default width (negatives shrinks width)
                        Tracer = new TracerBaseDef
                        {
                            Enable = false,
                            Length = 2f,
                            Width = 0.02f,
                            Color = Color(red: 100f, green: 50f, blue: 50f, alpha: 25),
                        },
                        Trail = new TrailDef
                        {
                            Enable = false,
                            Material = "WeaponLaser",
                            DecayTime = 128,
                            Color = Color(red: 0, green: 0, blue: 1, alpha: 1),
                            Back = true,
                            CustomWidth = 0,
                            UseWidthVariance = false,
                            UseColorFade = true,
                        },
                        OffsetEffect = new OffsetEffectDef
                        {
                            MaxOffset =  0,// 0 offset value disables this effect
                            MinLength = 0.2f,
                            MaxLength = 3,
                        },
                    },
                },
                AmmoAudio = new AmmoAudioDef
                {
                    TravelSound = "ChimeraFire",
                    HitSound = "",
                    HitPlayChance = 0.1f,
                    HitPlayShield = true,
                }, // Don't edit below this line

        };
		private AmmoDef ChimeraFire4 => new AmmoDef
        {
            AmmoMagazine = "ChimeraTank",
            AmmoRound = "ChimeraFire4",
            HybridRound = false, //AmmoMagazine based weapon with energy cost
            EnergyCost = 0.00000000001f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
            BaseDamage = 10f,
            Mass = 0f, // in kilograms
            Health = 0, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying.
            BackKickForce = 10f,
            DecayPerShot = 0f,
            HardPointUsable = false, // set to false if this is a Fragment ammoType and you don't want the turret to be able to select it directly.
            EnergyMagazineSize = 12, //ONLY USE IF "AmmoMagazine=ENERGY". Otherwise specify magazine size in Ammo.sbc file.
            IgnoreWater = false,

            Shape = new ShapeDef //defines the collision shape of projectile, defaults line and visual Line Length if set to 0
                {
                    Shape = LineShape,
                    Diameter = 1,
                },
                ObjectsHit = new ObjectsHitDef
                {
                    MaxObjectsHit = 0, // 0 = disabled
                    CountBlocks = false, // counts gridBlocks and not just entities hit
                },
                Fragment = new FragmentDef
				{
					AmmoRound = "ChimeraFire",
					Fragments = 1, //Number of Fragment projectiles created on impact.
					Degrees = 1,  //Think of this as a cone in front of the projectile, 360 degrees for a full circle.
					Reverse = false, //Redirects the cone back towards the projectile's rear
					RandomizeDir = false,
				},
                DamageScales = new DamageScaleDef
                {
                    MaxIntegrity = 0f, // 0 = disabled, 1000 = any blocks with currently integrity above 1000 will be immune to damage.
                    DamageVoxels = false, // true = voxels are vulnerable to this weapon
                    SelfDamage = false, // true = allow self damage.
                    HealthHitModifier = 1, // defaults to a value of 1, this setting modifies how much Health is subtracted from a projectile per hit (1 = per hit).
                    VoxelHitModifier = 3,  // modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01 = 1% damage, 2 = 200% damage.
                    Characters = 0.2f, //Damage to characters. 1f=same as damage to grids
                    Grids = new GridSizeDef
                    {
                        Large = -1f,
                        Small = .5f,
                    },
                    Armor = new ArmorDef
                    {
                        Armor = .25f,
                        Light = -1f,
                        Heavy = -1f,
                        NonArmor = 1f,
                    },
                    Shields = new ShieldDef
                    {
                        Modifier = .5f,
                        Type = Default,
                        BypassModifier = -1f,
                    },
                    // first true/false (ignoreOthers) will cause projectiles to pass through all blocks that do not match the custom subtypeIds.
                    Custom = new CustomScalesDef
                    {
                        IgnoreAllOthers = false,
                        Types = new []
                        {
                            new CustomBlocksDef
                            {
                                SubTypeId = "Test1",
                                Modifier = -1f,
                            },
                            new CustomBlocksDef
                            {
                                SubTypeId = "Test2",
                                Modifier = -1f,
                            },
                        },
                    },
                },
                AreaEffect = new AreaDamageDef
                {
                    AreaEffect = Radiant, // Disabled = do not use area effect at all, Explosive, Radiant, AntiSmart, JumpNullField, JumpNullField, EnergySinkField, AnchorField, EmpField, OffenseField, NavField, DotField.
                    AreaEffectDamage = 30f, // 0 = DO NOT USE FOR EXPLOSIVES, USE DETONATION SETTINGS INSTEAD
                    AreaEffectRadius = 5f, // DO NOT USE FOR EXPLOSIVES, USE DETONATION SETTINGS INSTEAD
                    Pulse = new PulseDef // interval measured in game ticks (60 == 1 second), pulseChance chance (0 - 100) that an entity in field will be hit
                    {
                        Interval = 30,
                        PulseChance = 0,
                    },
                    Explosions = new ExplosionDef
                    {
                        NoVisuals = false, //Set to false to use vanilla explosion particles, unless specified a custom particle
                        NoSound = false, ////Set to false to use vanilla explosion noises, unless specified a custom sound
                        Scale = 2, //Scale of the particles.
                        CustomParticle = "",
                        CustomSound = "",
                    },
                    Detonation = new DetonateDef //USE THESE MODIFIERS FOR EXPLOSIONS
                    {
                        DetonateOnEnd = true,
                        ArmOnlyOnHit = false,
                        DetonationDamage = 0, //1 Steelplate =100HP, 1 large light armor cube=2500HP. Has a natural falloff based on radius.
                        DetonationRadius = 5, //Measured in meters, how large the explosion should be.
                    },
                    EwarFields = new EwarFieldsDef
                    {
                        Duration = 0,
                        StackDuration = true,
                        Depletable = false,
                        MaxStacks = 10,
                        TriggerRange = 5f,
                    },
                },
                Beams = new BeamDef
                {
                    Enable = false,
                    VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
                    ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
                    RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
                    OneParticle = false, // Only spawn one particle hit per beam weapon.
                },
                Trajectory = new TrajectoryDef
                {
                    Guidance = None,
                    TargetLossDegree = 180f,
                    TargetLossTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    MaxLifeTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    AccelPerSec = 0f,
                    DesiredSpeed = 40, //Speed of the projectile. DO NOT SET HIGHER THAN 4100.
                    MaxTrajectory = 40f, //Maximum distance the projectile can travel before detonating/despawning.
                    FieldTime = 0, // 0 is disabled, a value causes the projectile to come to rest, spawn a field and remain for a time (Measured in game ticks, 60 = 1 second)
					GravityMultiplier = 1f, //1f=Gravity applies normal pull to projectile. 0 to disable.
                    SpeedVariance = Random(start: 0, end: 0), // subtracts value from DesiredSpeed
                    RangeVariance = Random(start: 0, end: 0), // subtracts value from MaxTrajectory
                    Smarts = new SmartsDef
                    {
                        Inaccuracy = 5f, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
                        Aggressiveness = 0.1f, // controls how responsive tracking is.
                        MaxLateralThrust = 0.05f, // controls how sharp the trajectile may turn
                        TrackingDelay = 400, // Measured in Shape diameter units traveled.
                        MaxChaseTime = 2000, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                        OverideTarget = true, // when set to true ammo picks its own target, does not use hardpoint's.
                    },
                    Mines = new MinesDef
                    {
                        DetectRadius =  200,
                        DeCloakRadius = 100,
                        FieldTime = 1800,
                        Cloak = true,
                        Persist = false,
                    },
                },
                AmmoGraphics = new GraphicDef
                {
                    ModelName = "", //USE DOUBLE LINES, ex: "\\Models\\Name\\Small\\MyGun.mwm"
                    VisualProbability = 1f, //Percentage chance for the model to spawn, "1f" for 100% chance.
                    ShieldHitDraw = true,
                    Particles = new AmmoParticleDef
                    {
                        Ammo = new ParticleDef
                        {
                            Name = "Chimera4", //Particle that surrounds your projectile. ShipWelderArc
                            Color = Color(red: 1f, green: 1f, blue: 1f, alpha: 1),
                            Offset = Vector(x: 0, y: 0, z: 0),
                            Extras = new ParticleOptionDef
                            {
                                Loop = true,
                                Restart = false,
                                MaxDistance = 5000,
                                MaxDuration = 3,
                                Scale = .9f,
                            },
                        },
                        Hit = new ParticleDef
                        {
                            Name = "ChimeraFire",
                            ApplyToShield = true,
                            ShrinkByDistance = true,
                            Color = Color(red: 3, green: 2, blue: 1, alpha: 1),
                            Offset = Vector(x: 0, y: 0, z: 0),
                            Extras = new ParticleOptionDef
                            {
                                Loop = false,
                                Restart = false,
                                MaxDistance = 500,
                                MaxDuration = 1,
                                Scale = 1,
                                HitPlayChance = 1f,
                            },
                        },
                    },
                    Lines = new LineDef
                    {
                        TracerMaterial = "ProjectileTrailLine", // Particle that follows your projectile. WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                        ColorVariance = Random(start: 5f, end: 10f), // multiply the color by random values within range.
                        WidthVariance = Random(start: 0f, end: 0.045f), // adds random value to default width (negatives shrinks width)
                        Tracer = new TracerBaseDef
                        {
                            Enable = false,
                            Length = 2f,
                            Width = 0.02f,
                            Color = Color(red: 100f, green: 50f, blue: 50f, alpha: 25),
                        },
                        Trail = new TrailDef
                        {
                            Enable = false,
                            Material = "WeaponLaser",
                            DecayTime = 128,
                            Color = Color(red: 0, green: 0, blue: 1, alpha: 1),
                            Back = true,
                            CustomWidth = 0,
                            UseWidthVariance = false,
                            UseColorFade = true,
                        },
                        OffsetEffect = new OffsetEffectDef
                        {
                            MaxOffset =  0,// 0 offset value disables this effect
                            MinLength = 0.2f,
                            MaxLength = 3,
                        },
                    },
                },
                AmmoAudio = new AmmoAudioDef
                {
                    TravelSound = "ChimeraFire",
                    HitSound = "",
                    HitPlayChance = 0.1f,
                    HitPlayShield = true,
                }, // Don't edit below this line

        };
		private AmmoDef ChimeraFire => new AmmoDef
        {
            AmmoMagazine = "ChimeraTank",
            AmmoRound = "ChimeraFire",
            HybridRound = false, //AmmoMagazine based weapon with energy cost
            EnergyCost = 0.00000000001f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
            BaseDamage = 10f,
            Mass = 0f, // in kilograms
            Health = 0, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying.
            BackKickForce = 10f,
            DecayPerShot = 0f,
            HardPointUsable = false, // set to false if this is a Fragment ammoType and you don't want the turret to be able to select it directly.
            EnergyMagazineSize = 12, //ONLY USE IF "AmmoMagazine=ENERGY". Otherwise specify magazine size in Ammo.sbc file.
            IgnoreWater = false,

            Shape = new ShapeDef //defines the collision shape of projectile, defaults line and visual Line Length if set to 0
                {
                    Shape = LineShape,
                    Diameter = 1,
                },
                ObjectsHit = new ObjectsHitDef
                {
                    MaxObjectsHit = 0, // 0 = disabled
                    CountBlocks = false, // counts gridBlocks and not just entities hit
                },
                Fragment = new FragmentDef
				{
					AmmoRound = "",
					Fragments = 25, //Number of Fragment projectiles created on impact.
					Degrees = 180,  //Think of this as a cone in front of the projectile, 360 degrees for a full circle.
					Reverse = false, //Redirects the cone back towards the projectile's rear
					RandomizeDir = false,
				},
                DamageScales = new DamageScaleDef
                {
                    MaxIntegrity = 0f, // 0 = disabled, 1000 = any blocks with currently integrity above 1000 will be immune to damage.
                    DamageVoxels = false, // true = voxels are vulnerable to this weapon
                    SelfDamage = false, // true = allow self damage.
                    HealthHitModifier = 1, // defaults to a value of 1, this setting modifies how much Health is subtracted from a projectile per hit (1 = per hit).
                    VoxelHitModifier = 3,  // modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01 = 1% damage, 2 = 200% damage.
                    Characters = 0.2f, //Damage to characters. 1f=same as damage to grids
                    Grids = new GridSizeDef
                    {
                        Large = -1f,
                        Small = .5f,
                    },
                    Armor = new ArmorDef
                    {
                        Armor = .25f,
                        Light = -1f,
                        Heavy = -1f,
                        NonArmor = 1f,
                    },
                    Shields = new ShieldDef
                    {
                        Modifier = .5f,
                        Type = Default,
                        BypassModifier = -1f,
                    },
                    // first true/false (ignoreOthers) will cause projectiles to pass through all blocks that do not match the custom subtypeIds.
                    Custom = new CustomScalesDef
                    {
                        IgnoreAllOthers = false,
                        Types = new []
                        {
                            new CustomBlocksDef
                            {
                                SubTypeId = "Test1",
                                Modifier = -1f,
                            },
                            new CustomBlocksDef
                            {
                                SubTypeId = "Test2",
                                Modifier = -1f,
                            },
                        },
                    },
                },
                AreaEffect = new AreaDamageDef
                {
                    AreaEffect = Radiant, // Disabled = do not use area effect at all, Explosive, Radiant, AntiSmart, JumpNullField, JumpNullField, EnergySinkField, AnchorField, EmpField, OffenseField, NavField, DotField.
                    AreaEffectDamage = 30f, // 0 = DO NOT USE FOR EXPLOSIVES, USE DETONATION SETTINGS INSTEAD
                    AreaEffectRadius = 5f, // DO NOT USE FOR EXPLOSIVES, USE DETONATION SETTINGS INSTEAD
                    Pulse = new PulseDef // interval measured in game ticks (60 == 1 second), pulseChance chance (0 - 100) that an entity in field will be hit
                    {
                        Interval = 30,
                        PulseChance = 0,
                    },
                    Explosions = new ExplosionDef
                    {
                        NoVisuals = false, //Set to false to use vanilla explosion particles, unless specified a custom particle
                        NoSound = false, ////Set to false to use vanilla explosion noises, unless specified a custom sound
                        Scale = 2, //Scale of the particles.
                        CustomParticle = "",
                        CustomSound = "",
                    },
                    Detonation = new DetonateDef //USE THESE MODIFIERS FOR EXPLOSIONS
                    {
                        DetonateOnEnd = true,
                        ArmOnlyOnHit = false,
                        DetonationDamage = 0, //1 Steelplate =100HP, 1 large light armor cube=2500HP. Has a natural falloff based on radius.
                        DetonationRadius = 5, //Measured in meters, how large the explosion should be.
                    },
                    EwarFields = new EwarFieldsDef
                    {
                        Duration = 0,
                        StackDuration = true,
                        Depletable = false,
                        MaxStacks = 10,
                        TriggerRange = 5f,
                    },
                },
                Beams = new BeamDef
                {
                    Enable = false,
                    VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
                    ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
                    RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
                    OneParticle = false, // Only spawn one particle hit per beam weapon.
                },
                Trajectory = new TrajectoryDef
                {
                    Guidance = None,
                    TargetLossDegree = 180f,
                    TargetLossTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    MaxLifeTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    AccelPerSec = 0f,
                    DesiredSpeed = 40, //Speed of the projectile. DO NOT SET HIGHER THAN 4100.
                    MaxTrajectory = 40f, //Maximum distance the projectile can travel before detonating/despawning.
                    FieldTime = 0, // 0 is disabled, a value causes the projectile to come to rest, spawn a field and remain for a time (Measured in game ticks, 60 = 1 second)
					GravityMultiplier = 1f, //1f=Gravity applies normal pull to projectile. 0 to disable.
                    SpeedVariance = Random(start: 0, end: 0), // subtracts value from DesiredSpeed
                    RangeVariance = Random(start: 0, end: 0), // subtracts value from MaxTrajectory
                    Smarts = new SmartsDef
                    {
                        Inaccuracy = 5f, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
                        Aggressiveness = 0.1f, // controls how responsive tracking is.
                        MaxLateralThrust = 0.05f, // controls how sharp the trajectile may turn
                        TrackingDelay = 400, // Measured in Shape diameter units traveled.
                        MaxChaseTime = 2000, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                        OverideTarget = true, // when set to true ammo picks its own target, does not use hardpoint's.
                    },
                    Mines = new MinesDef
                    {
                        DetectRadius =  200,
                        DeCloakRadius = 100,
                        FieldTime = 1800,
                        Cloak = true,
                        Persist = false,
                    },
                },
                AmmoGraphics = new GraphicDef
                {
                    ModelName = "", //USE DOUBLE LINES, ex: "\\Models\\Name\\Small\\MyGun.mwm"
                    VisualProbability = 1f, //Percentage chance for the model to spawn, "1f" for 100% chance.
                    ShieldHitDraw = true,
                    Particles = new AmmoParticleDef
                    {
                        Ammo = new ParticleDef
                        {
                            Name = "Chimera5", //Particle that surrounds your projectile. ShipWelderArc
                            Color = Color(red: 1f, green: 1f, blue: 1f, alpha: 1),
                            Offset = Vector(x: 0, y: 0, z: 0),
                            Extras = new ParticleOptionDef
                            {
                                Loop = true,
                                Restart = false,
                                MaxDistance = 5000,
                                MaxDuration = 3,
                                Scale = 1f,
                            },
                        },
                        Hit = new ParticleDef
                        {
                            Name = "ChimeraFire",
                            ApplyToShield = true,
                            ShrinkByDistance = true,
                            Color = Color(red: 3, green: 2, blue: 1, alpha: 1),
                            Offset = Vector(x: 0, y: 0, z: 0),
                            Extras = new ParticleOptionDef
                            {
                                Loop = false,
                                Restart = false,
                                MaxDistance = 500,
                                MaxDuration = 1,
                                Scale = 1,
                                HitPlayChance = 1f,
                            },
                        },
                    },
                    Lines = new LineDef
                    {
                        TracerMaterial = "ProjectileTrailLine", // Particle that follows your projectile. WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                        ColorVariance = Random(start: 5f, end: 10f), // multiply the color by random values within range.
                        WidthVariance = Random(start: 0f, end: 0.045f), // adds random value to default width (negatives shrinks width)
                        Tracer = new TracerBaseDef
                        {
                            Enable = false,
                            Length = 2f,
                            Width = 0.02f,
                            Color = Color(red: 100f, green: 50f, blue: 50f, alpha: 25),
                        },
                        Trail = new TrailDef
                        {
                            Enable = false,
                            Material = "WeaponLaser",
                            DecayTime = 128,
                            Color = Color(red: 0, green: 0, blue: 1, alpha: 1),
                            Back = true,
                            CustomWidth = 0,
                            UseWidthVariance = false,
                            UseColorFade = true,
                        },
                        OffsetEffect = new OffsetEffectDef
                        {
                            MaxOffset =  0,// 0 offset value disables this effect
                            MinLength = 0.2f,
                            MaxLength = 3,
                        },
                    },
                },
                AmmoAudio = new AmmoAudioDef
                {
                    TravelSound = "ChimeraFire",
                    HitSound = "",
                    HitPlayChance = 0.1f,
                    HitPlayShield = true,
                }, // Don't edit below this line

        };
		
		private AmmoDef Plasma => new AmmoDef
        {
            AmmoMagazine = "Energy",
            AmmoRound = "Plasma",
            HybridRound = false, //AmmoMagazine based weapon with energy cost
            EnergyCost = .001f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
            BaseDamage = 10000f,
            Mass = 0f, // in kilograms
            Health = 0, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying.
            BackKickForce = 1000f,
            HardPointUsable = true, // set to false if this is a Fragment ammoType and you don't want the turret to be able to select it directly.
            EnergyMagazineSize = 3,

            Shape = new ShapeDef //defines the collision shape of projectile, defaults line and visual Line Length if set to 0
            {
                Shape = LineShape,
                Diameter = 0,
            },
            ObjectsHit = new ObjectsHitDef
            {
                MaxObjectsHit = 10, // 0 = disabled
                CountBlocks = false, // counts gridBlocks and not just entities hit
            },
            Fragment = new FragmentDef //FIX MANUALLY
            {
                AmmoRound = "",
                Fragments = 0,
                Degrees = 0,
                Reverse = false,
                RandomizeDir = false,
            },
            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0f, // 0 = disabled, 1000 = any blocks with currently integrity above 1000 will be immune to damage.
                DamageVoxels = true, // true = voxels are vulnerable to this weapon
                SelfDamage = false, // true = allow self damage.

                // modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01 = 1% damage, 2 = 200% damage.
                Characters = -1f,
                Grids = new GridSizeDef
                {
                    Large = 1f,
                    Small = 0.1f,
                },
                Armor = new ArmorDef
                {
                    Armor = 1f,
                    Light = 0.85f,
                    Heavy = 1f,
                    NonArmor = 2f,
                },
                Shields = new ShieldDef
                {
                    Modifier = 6.0f,
                    Type = Default,
                    BypassModifier = -1f,
                },
                // first true/false (ignoreOthers) will cause projectiles to pass through all blocks that do not match the custom subtypeIds.
                Custom = new CustomScalesDef
                {
                    IgnoreAllOthers = false,
                    Types = new[]
                    {
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test1",
                            Modifier = -1f,
                        },
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test2",
                            Modifier = -1f,
                        },
                    },
                },
            },
            AreaEffect = new AreaDamageDef
            {
                AreaEffect = Radiant, // Disabled = do not use area effect at all, Explosive, Radiant, AntiSmart, JumpNullField, JumpNullField, EnergySinkField, AnchorField, EmpField, OffenseField, NavField, DotField.
                AreaEffectDamage = 0, // 0 = use spillover from BaseDamage, otherwise use this value.
                AreaEffectRadius = 10f,
                Pulse = new PulseDef // interval measured in game ticks (60 == 1 second), pulseChance chance (0 - 100) that an entity in field will be hit
                {
                    Interval = 60,
                    PulseChance = 0,
                },
                Explosions = new ExplosionDef
                {
                    NoVisuals = false,
                    NoSound = false,
                    Scale = 1,
                    CustomParticle = "",
                    CustomSound = "",
                },
                Detonation = new DetonateDef
                {
                    DetonateOnEnd = false,
                    ArmOnlyOnHit = false,
                    DetonationDamage = 0,
                    DetonationRadius = 0,
                },
                EwarFields = new EwarFieldsDef //FIX Manually
                {
                    Duration = 60,
                    StackDuration = true,
                    Depletable = false,
                    MaxStacks = 10,
                    TriggerRange = 5f,
                },
            },
            Beams = new BeamDef
            {
                Enable = false,
                VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
                ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
                RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
                OneParticle = false, // Only spawn one particle hit per beam weapon.
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = None,
                TargetLossDegree = 80f,
                TargetLossTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                MaxLifeTime = 600, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                AccelPerSec = 0f,
                DesiredSpeed = 80f,
                MaxTrajectory = 5000f,
                FieldTime = 0,				// 0 is disabled, a value causes the projectile to come to rest, spawn a field and remain for a time (Measured in game ticks, 60 = 1 second)
				GravityMultiplier = .5f,
                SpeedVariance = Random(start: 0, end: 0), // subtracts value from DesiredSpeed
                RangeVariance = Random(start: 0, end: 0), // subtracts value from MaxTrajectory
                Smarts = new SmartsDef
                {
                    Inaccuracy = 2f, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
                    Aggressiveness = 1f, // controls how responsive tracking is.
                    MaxLateralThrust = 0.5, // controls how sharp the trajectile may turn
                    TrackingDelay = 0, // Measured in Shape diameter units traveled.
                    MaxChaseTime = 1000, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    OverideTarget = false, // when set to true ammo picks its own target, does not use hardpoint's.
                },
                Mines = new MinesDef
                {
                    DetectRadius = 200,
                    DeCloakRadius = 100,
                    FieldTime = 0,
                    Cloak = true,
                    Persist = false,
                },
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "",
                VisualProbability = 1f,
                ShieldHitDraw = true,
                Particles = new AmmoParticleDef
                {
                    Ammo = new ParticleDef
                    {
                        Name = "", //ShipWelderArc
                        Color = Color(red: 0, green: 60, blue: 200, alpha: 1),
                        Offset = Vector(x: 0, y: -1, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 1,
                            Scale = .5f,
                        },
                    },
                    Hit = new ParticleDef
                    {
                        Name = "PlasmaExplosion",
                        ApplyToShield = true,
                        ShrinkByDistance = true,
                        Color = Color(red: 0, green: 60, blue: 200, alpha: 1),
                        Offset = Vector(x: 0, y: 0, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 1,
                            Scale = 1f,
                            HitPlayChance = 1f,
                        },
                    },
                },
                Lines = new LineDef
                {
                    TracerMaterial = "ProjectileTrailLine", // WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                    ColorVariance = Random(start: 0.75f, end: 2f), // multiply the color by random values within range.
                    WidthVariance = Random(start: 0f, end: 0.15f), // adds random value to default width (negatives shrinks width)
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = 5f,
                        Width = .1f,
                        Color = Color(red: 0, green: 60, blue: 200, alpha: 1),
                    },
                    Trail = new TrailDef
                    {
                        Enable = true,
                        Material = "WeaponLaser",
                        DecayTime = 30,
                        Color = Color(red: 0, green: 60, blue: 200, alpha: 1),
                        Back = false,
                        CustomWidth = 0.1f,
                        UseWidthVariance = false,
                        UseColorFade = true,
                    },
                    OffsetEffect = new OffsetEffectDef
                    {
                        MaxOffset = 0,// 0 offset value disables this effect
                        MinLength = 4f,
                        MaxLength = 4f,
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                TravelSound = "",
                HitSound = "",
                HitPlayChance = 1.0f,
                HitPlayShield = true,
            }, // Don't edit below this line
        };
		
		private AmmoDef PlasmaBolt => new AmmoDef
        {
            AmmoMagazine = "Energy",
            AmmoRound = "PlasmaBolt",
            HybridRound = false, //AmmoMagazine based weapon with energy cost
            EnergyCost = .0005f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
            BaseDamage = 8000f,
            Mass = 0f, // in kilograms
            Health = 0, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying.
            BackKickForce = 1000f,
            HardPointUsable = true, // set to false if this is a Fragment ammoType and you don't want the turret to be able to select it directly.
            EnergyMagazineSize = 3,

            Shape = new ShapeDef //defines the collision shape of projectile, defaults line and visual Line Length if set to 0
            {
                Shape = LineShape,
                Diameter = 1,
            },
            ObjectsHit = new ObjectsHitDef
            {
                MaxObjectsHit = 10, // 0 = disabled
                CountBlocks = false, // counts gridBlocks and not just entities hit
            },
            Fragment = new FragmentDef //FIX MANUALLY
            {
                AmmoRound = "",
                Fragments = 0,
                Degrees = 0,
                Reverse = false,
                RandomizeDir = false,
            },
            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0f, // 0 = disabled, 1000 = any blocks with currently integrity above 1000 will be immune to damage.
                DamageVoxels = false, // true = voxels are vulnerable to this weapon
                SelfDamage = false, // true = allow self damage.

                // modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01 = 1% damage, 2 = 200% damage.
                Characters = -1f,
                Grids = new GridSizeDef
                {
                    Large = 1f,
                    Small = .5f,
                },
                Armor = new ArmorDef
                {
                    Armor = 1f,
                    Light = 1f,
                    Heavy = 1f,
                    NonArmor = 1f,
                },
                Shields = new ShieldDef
                {
                    Modifier = 3.0f,
                    Type = Default,
                    BypassModifier = -1f,
                },
                // first true/false (ignoreOthers) will cause projectiles to pass through all blocks that do not match the custom subtypeIds.
                Custom = new CustomScalesDef
                {
                    IgnoreAllOthers = false,
                    Types = new[]
                    {
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test1",
                            Modifier = -1f,
                        },
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test2",
                            Modifier = -1f,
                        },
                    },
                },
            },
            AreaEffect = new AreaDamageDef
            {
                AreaEffect = Radiant, // Disabled = do not use area effect at all, Explosive, Radiant, AntiSmart, JumpNullField, JumpNullField, EnergySinkField, AnchorField, EmpField, OffenseField, NavField, DotField.
                AreaEffectDamage = 0, // 0 = use spillover from BaseDamage, otherwise use this value.
                AreaEffectRadius = 5f,
                Pulse = new PulseDef // interval measured in game ticks (60 == 1 second), pulseChance chance (0 - 100) that an entity in field will be hit
                {
                    Interval = 60,
                    PulseChance = 0,
                },
                Explosions = new ExplosionDef
                {
                    NoVisuals = false,
                    NoSound = false,
                    Scale = 1,
                    CustomParticle = "",
                    CustomSound = "",
                },
                Detonation = new DetonateDef
                {
                    DetonateOnEnd = false,
                    ArmOnlyOnHit = false,
                    DetonationDamage = 0,
                    DetonationRadius = 0,
                },
                EwarFields = new EwarFieldsDef //FIX Manually
                {
                    Duration = 60,
                    StackDuration = true,
                    Depletable = false,
                    MaxStacks = 10,
                    TriggerRange = 5f,
                },
            },
            Beams = new BeamDef
            {
                Enable = false,
                VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
                ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
                RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
                OneParticle = false, // Only spawn one particle hit per beam weapon.
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = None,
                TargetLossDegree = 80f,
                TargetLossTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                MaxLifeTime = 600, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                AccelPerSec = 0f,
                DesiredSpeed = 300f,
                MaxTrajectory = 5000f,
                FieldTime = 0,				// 0 is disabled, a value causes the projectile to come to rest, spawn a field and remain for a time (Measured in game ticks, 60 = 1 second)
				GravityMultiplier = 0,
                SpeedVariance = Random(start: 0, end: 0), // subtracts value from DesiredSpeed
                RangeVariance = Random(start: 0, end: 0), // subtracts value from MaxTrajectory
                Smarts = new SmartsDef
                {
                    Inaccuracy = 2f, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
                    Aggressiveness = 1f, // controls how responsive tracking is.
                    MaxLateralThrust = 0.5, // controls how sharp the trajectile may turn
                    TrackingDelay = 0, // Measured in Shape diameter units traveled.
                    MaxChaseTime = 1000, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    OverideTarget = false, // when set to true ammo picks its own target, does not use hardpoint's.
                },
                Mines = new MinesDef
                {
                    DetectRadius = 200,
                    DeCloakRadius = 100,
                    FieldTime = 0,
                    Cloak = true,
                    Persist = false,
                },
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "",
                VisualProbability = 1f,
                ShieldHitDraw = true,
                Particles = new AmmoParticleDef
                {
                    Ammo = new ParticleDef
                    {
                        Name = "", //ShipWelderArc
                        Color = Color(red: 0, green: 200, blue: 100, alpha: 32),
                        Offset = Vector(x: 0, y: -1, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 1,
                            Scale = .5f,
                        },
                    },
                    Hit = new ParticleDef
                    {
                        Name = "PlasmaBolt",
                        ApplyToShield = true,
                        ShrinkByDistance = true,
                        Color = Color(red: 200, green: 60, blue: 50, alpha: 1),
                        Offset = Vector(x: 0, y: 0, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 1,
                            Scale = .1f,
                            HitPlayChance = 1f,
                        },
                    },
                },
                Lines = new LineDef
                {
                    TracerMaterial = "ProjectileTrailLine", // WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                    ColorVariance = Random(start: 0.75f, end: 2f), // multiply the color by random values within range.
                    WidthVariance = Random(start: 0f, end: 0.15f), // adds random value to default width (negatives shrinks width)
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = 1f,
                        Width = .1f,
                        Color = Color(red: 200, green: 70, blue: 30, alpha: 1),
                    },
                    Trail = new TrailDef
                    {
                        Enable = true,
                        Material = "WeaponLaser",
                        DecayTime = 1,
                        Color = Color(red: 200, green: 70, blue: 30, alpha: 1),
                        Back = false,
                        CustomWidth = 0.1f,
                        UseWidthVariance = false,
                        UseColorFade = true,
                    },
                    OffsetEffect = new OffsetEffectDef
                    {
                        MaxOffset = 0,// 0 offset value disables this effect
                        MinLength = 4f,
                        MaxLength = 4f,
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                TravelSound = "",
                HitSound = "",
                HitPlayChance = 1.0f,
                HitPlayShield = true,
            }, // Don't edit below this line
        };
		
		private AmmoDef RepeaterBolt => new AmmoDef
        {
            AmmoMagazine = "Energy",
            AmmoRound = "PlasmaBolt",
            HybridRound = false, //AmmoMagazine based weapon with energy cost
            EnergyCost = .0025f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
            BaseDamage = 3000f,
            Mass = 5f, // in kilograms
            Health = 0, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying.
            BackKickForce = 1000f,
            HardPointUsable = true, // set to false if this is a Fragment ammoType and you don't want the turret to be able to select it directly.
            EnergyMagazineSize = 3,

            Shape = new ShapeDef //defines the collision shape of projectile, defaults line and visual Line Length if set to 0
            {
                Shape = LineShape,
                Diameter = 1,
            },
            ObjectsHit = new ObjectsHitDef
            {
                MaxObjectsHit = 10, // 0 = disabled
                CountBlocks = false, // counts gridBlocks and not just entities hit
            },
            Fragment = new FragmentDef //FIX MANUALLY
            {
                AmmoRound = "",
                Fragments = 0,
                Degrees = 0,
                Reverse = false,
                RandomizeDir = false,
            },
            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0f, // 0 = disabled, 1000 = any blocks with currently integrity above 1000 will be immune to damage.
                DamageVoxels = false, // true = voxels are vulnerable to this weapon
                SelfDamage = false, // true = allow self damage.

                // modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01 = 1% damage, 2 = 200% damage.
                Characters = -1f,
                Grids = new GridSizeDef
                {
                    Large = 1f,
                    Small = .5f,
                },
                Armor = new ArmorDef
                {
                    Armor = 1f,
                    Light = .85f,
                    Heavy = .25f,
                    NonArmor = 2f,
                },
                Shields = new ShieldDef
                {
                    Modifier = 3.0f,
                    Type = Default,
                    BypassModifier = -1f,
                },
                // first true/false (ignoreOthers) will cause projectiles to pass through all blocks that do not match the custom subtypeIds.
                Custom = new CustomScalesDef
                {
                    IgnoreAllOthers = false,
                    Types = new[]
                    {
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test1",
                            Modifier = -1f,
                        },
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test2",
                            Modifier = -1f,
                        },
                    },
                },
            },
            AreaEffect = new AreaDamageDef
            {
                AreaEffect = Radiant, // Disabled = do not use area effect at all, Explosive, Radiant, AntiSmart, JumpNullField, JumpNullField, EnergySinkField, AnchorField, EmpField, OffenseField, NavField, DotField.
                AreaEffectDamage = 0, // 0 = use spillover from BaseDamage, otherwise use this value.
                AreaEffectRadius = 2f,
                Pulse = new PulseDef // interval measured in game ticks (60 == 1 second), pulseChance chance (0 - 100) that an entity in field will be hit
                {
                    Interval = 60,
                    PulseChance = 0,
                },
                Explosions = new ExplosionDef
                {
                    NoVisuals = false,
                    NoSound = false,
                    Scale = 1,
                    CustomParticle = "",
                    CustomSound = "",
                },
                Detonation = new DetonateDef
                {
                    DetonateOnEnd = false,
                    ArmOnlyOnHit = false,
                    DetonationDamage = 0,
                    DetonationRadius = 0,
                },
                EwarFields = new EwarFieldsDef //FIX Manually
                {
                    Duration = 60,
                    StackDuration = true,
                    Depletable = false,
                    MaxStacks = 10,
                    TriggerRange = 5f,
                },
            },
            Beams = new BeamDef
            {
                Enable = false,
                VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
                ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
                RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
                OneParticle = false, // Only spawn one particle hit per beam weapon.
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = None,
                TargetLossDegree = 80f,
                TargetLossTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                MaxLifeTime = 600, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                AccelPerSec = 0f,
                DesiredSpeed = 200f,
                MaxTrajectory = 5000f,
                FieldTime = 0,				// 0 is disabled, a value causes the projectile to come to rest, spawn a field and remain for a time (Measured in game ticks, 60 = 1 second)
				GravityMultiplier = 0,
                SpeedVariance = Random(start: 0, end: 0), // subtracts value from DesiredSpeed
                RangeVariance = Random(start: 0, end: 0), // subtracts value from MaxTrajectory
                Smarts = new SmartsDef
                {
                    Inaccuracy = 2f, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
                    Aggressiveness = 1f, // controls how responsive tracking is.
                    MaxLateralThrust = 0.5, // controls how sharp the trajectile may turn
                    TrackingDelay = 0, // Measured in Shape diameter units traveled.
                    MaxChaseTime = 1000, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    OverideTarget = false, // when set to true ammo picks its own target, does not use hardpoint's.
                },
                Mines = new MinesDef
                {
                    DetectRadius = 200,
                    DeCloakRadius = 100,
                    FieldTime = 0,
                    Cloak = true,
                    Persist = false,
                },
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "",
                VisualProbability = 1f,
                ShieldHitDraw = true,
                Particles = new AmmoParticleDef
                {
                    Ammo = new ParticleDef
                    {
                        Name = "", //ShipWelderArc
                        Color = Color(red: 0, green: 200, blue: 100, alpha: 32),
                        Offset = Vector(x: 0, y: -1, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 1,
                            Scale = .5f,
                        },
                    },
                    Hit = new ParticleDef
                    {
                        Name = "ArbalestBlast",
                        ApplyToShield = true,
                        ShrinkByDistance = true,
                        Color = Color(red: 0, green: 60, blue: 200, alpha: 1),
                        Offset = Vector(x: 0, y: 0, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 1,
                            Scale = .5f,
                            HitPlayChance = 1f,
                        },
                    },
                },
                Lines = new LineDef
                {
                    TracerMaterial = "ProjectileTrailLine", // WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                    ColorVariance = Random(start: 0.75f, end: 2f), // multiply the color by random values within range.
                    WidthVariance = Random(start: 0f, end: 0.15f), // adds random value to default width (negatives shrinks width)
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = .5f,
                        Width = .1f,
                        Color = Color(red: 0, green: 70, blue: 200, alpha: 1),
                    },
                    Trail = new TrailDef
                    {
                        Enable = true,
                        Material = "WeaponLaser",
                        DecayTime = 1,
                        Color = Color(red: 0, green: 70, blue: 200, alpha: 1),
                        Back = false,
                        CustomWidth = 0.1f,
                        UseWidthVariance = false,
                        UseColorFade = true,
                    },
                    OffsetEffect = new OffsetEffectDef
                    {
                        MaxOffset = 0,// 0 offset value disables this effect
                        MinLength = 4f,
                        MaxLength = 4f,
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                TravelSound = "",
                HitSound = "",
                HitPlayChance = 1.0f,
                HitPlayShield = true,
            }, // Don't edit below this line
        };
        
		private AmmoDef HeavyPlasmaBolt => new AmmoDef
        {
            AmmoMagazine = "Energy",
            AmmoRound = "HeavyPlasmaBolt",
            HybridRound = false, //AmmoMagazine based weapon with energy cost
            EnergyCost = .008f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
            BaseDamage = 18000f,
            Mass = 0f, // in kilograms
            Health = 0, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying.
            BackKickForce = 4000f,
            HardPointUsable = true, // set to false if this is a Fragment ammoType and you don't want the turret to be able to select it directly.
            EnergyMagazineSize = 3,

            Shape = new ShapeDef //defines the collision shape of projectile, defaults line and visual Line Length if set to 0
            {
                Shape = LineShape,
                Diameter = 1,
            },
            ObjectsHit = new ObjectsHitDef
            {
                MaxObjectsHit = 10, // 0 = disabled
                CountBlocks = false, // counts gridBlocks and not just entities hit
            },
            Fragment = new FragmentDef //FIX MANUALLY
            {
                AmmoRound = "",
                Fragments = 0,
                Degrees = 0,
                Reverse = false,
                RandomizeDir = false,
            },
            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0f, // 0 = disabled, 1000 = any blocks with currently integrity above 1000 will be immune to damage.
                DamageVoxels = false, // true = voxels are vulnerable to this weapon
                SelfDamage = false, // true = allow self damage.

                // modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01 = 1% damage, 2 = 200% damage.
                Characters = -1f,
				FallOff = new FallOffDef
                {
                    Distance = 800f, // Distance at which max damage begins falling off.
                    MinMultipler = 0.5f, // value from 0.0f to 1f where 0.1f would be a min damage of 10% of max damage.
                },
                Grids = new GridSizeDef
                {
                    Large = 1f,
                    Small = 5f,
                },
                Armor = new ArmorDef
                {
                    Armor = 1f,
                    Light = 1f,
                    Heavy = 1f,
                    NonArmor = 1f,
                },
                Shields = new ShieldDef
                {
                    Modifier = 4.0f,
                    Type = Default,
                    BypassModifier = -1f,
                },
                // first true/false (ignoreOthers) will cause projectiles to pass through all blocks that do not match the custom subtypeIds.
                Custom = new CustomScalesDef
                {
                    IgnoreAllOthers = false,
                    Types = new[]
                    {
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test1",
                            Modifier = -1f,
                        },
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test2",
                            Modifier = -1f,
                        },
                    },
                },
            },
            AreaEffect = new AreaDamageDef
            {
                AreaEffect = Radiant, // Disabled = do not use area effect at all, Explosive, Radiant, AntiSmart, JumpNullField, JumpNullField, EnergySinkField, AnchorField, EmpField, OffenseField, NavField, DotField.
                AreaEffectDamage = 1000, // 0 = use spillover from BaseDamage, otherwise use this value.
                AreaEffectRadius = 5f,
                Pulse = new PulseDef // interval measured in game ticks (60 == 1 second), pulseChance chance (0 - 100) that an entity in field will be hit
                {
                    Interval = 60,
                    PulseChance = 0,
                },
                Explosions = new ExplosionDef
                {
                    NoVisuals = false,
                    NoSound = false,
                    Scale = 1,
                    CustomParticle = "",
                    CustomSound = "",
                },
                Detonation = new DetonateDef
                {
                    DetonateOnEnd = false,
                    ArmOnlyOnHit = false,
                    DetonationDamage = 0,
                    DetonationRadius = 0,
                },
                EwarFields = new EwarFieldsDef //FIX Manually
                {
                    Duration = 60,
                    StackDuration = true,
                    Depletable = false,
                    MaxStacks = 10,
                    TriggerRange = 5f,
                },
            },
            Beams = new BeamDef
            {
                Enable = false,
                VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
                ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
                RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
                OneParticle = false, // Only spawn one particle hit per beam weapon.
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = None,
                TargetLossDegree = 80f,
                TargetLossTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                MaxLifeTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                AccelPerSec = 0f,
                DesiredSpeed = 300f,
                MaxTrajectory = 5000f,
                FieldTime = 0,				// 0 is disabled, a value causes the projectile to come to rest, spawn a field and remain for a time (Measured in game ticks, 60 = 1 second)
				GravityMultiplier = 0,
                SpeedVariance = Random(start: 0, end: 0), // subtracts value from DesiredSpeed
                RangeVariance = Random(start: 0, end: 0), // subtracts value from MaxTrajectory
                Smarts = new SmartsDef
                {
                    Inaccuracy = 2f, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
                    Aggressiveness = 1f, // controls how responsive tracking is.
                    MaxLateralThrust = 0.5, // controls how sharp the trajectile may turn
                    TrackingDelay = 0, // Measured in Shape diameter units traveled.
                    MaxChaseTime = 1000, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    OverideTarget = false, // when set to true ammo picks its own target, does not use hardpoint's.
                },
                Mines = new MinesDef
                {
                    DetectRadius = 200,
                    DeCloakRadius = 100,
                    FieldTime = 0,
                    Cloak = true,
                    Persist = false,
                },
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "\\Models\\Akiad\\Small\\OrionGlobe.mwm",
                VisualProbability = 1f,
                ShieldHitDraw = true,
                Particles = new AmmoParticleDef
                {
                    Ammo = new ParticleDef
                    {
                        Name = "", //ShipWelderArc
                        Color = Color(red: 0, green: 200, blue: 100, alpha: 32),
                        Offset = Vector(x: 0, y: -1, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 1,
                            Scale = .5f,
                        },
                    },
                    Hit = new ParticleDef
                    {
                        Name = "PlasmaBolt",
                        ApplyToShield = true,
                        ShrinkByDistance = true,
                        Color = Color(red: 200, green: 80, blue: 80, alpha: 1),
                        Offset = Vector(x: 0, y: 0, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 1,
                            Scale = .5f,
                            HitPlayChance = 1f,
                        },
                    },
                },
                Lines = new LineDef
                {
                    TracerMaterial = "ProjectileTrailLine", // WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                    ColorVariance = Random(start: 0.75f, end: 2f), // multiply the color by random values within range.
                    WidthVariance = Random(start: 0f, end: 0.15f), // adds random value to default width (negatives shrinks width)
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = 1f,
                        Width = .1f,
                        Color = Color(red: 200, green: 60, blue: 60, alpha: 1),
                    },
                    Trail = new TrailDef
                    {
                        Enable = true,
                        Material = "WeaponLaser",
                        DecayTime = 1,
                        Color = Color(red: 200, green: 0, blue: 00, alpha: 1),
                        Back = false,
                        CustomWidth = 0.1f,
                        UseWidthVariance = false,
                        UseColorFade = true,
                    },
                    OffsetEffect = new OffsetEffectDef
                    {
                        MaxOffset = 0,// 0 offset value disables this effect
                        MinLength = 4f,
                        MaxLength = 4f,
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                TravelSound = "",
                HitSound = "",
                HitPlayChance = 1.0f,
                HitPlayShield = true,
            }, // Don't edit below this line
        };
		private AmmoDef IcarusBolt => new AmmoDef
        {
            AmmoMagazine = "Energy",
            AmmoRound = "IcarusBolt",
            HybridRound = false, //AmmoMagazine based weapon with energy cost
            EnergyCost = .0008f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
            BaseDamage = 66000f,
            Mass = 0f, // in kilograms
            Health = 0, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying.
            BackKickForce = 4000f,
            HardPointUsable = true, // set to false if this is a Fragment ammoType and you don't want the turret to be able to select it directly.
            EnergyMagazineSize = 2,

            Shape = new ShapeDef //defines the collision shape of projectile, defaults line and visual Line Length if set to 0
            {
                Shape = LineShape,
                Diameter = 1,
            },
            ObjectsHit = new ObjectsHitDef
            {
                MaxObjectsHit = 0, // 0 = disabled
                CountBlocks = false, // counts gridBlocks and not just entities hit
            },
            Fragment = new FragmentDef //FIX MANUALLY
            {
                AmmoRound = "",
                Fragments = 0,
                Degrees = 0,
                Reverse = false,
                RandomizeDir = false,
            },
            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0f, // 0 = disabled, 1000 = any blocks with currently integrity above 1000 will be immune to damage.
                DamageVoxels = false, // true = voxels are vulnerable to this weapon
                SelfDamage = false, // true = allow self damage.

                // modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01 = 1% damage, 2 = 200% damage.
                Characters = -1f,
				FallOff = new FallOffDef
                {
                    Distance = 7000f, // Distance at which max damage begins falling off.
                    MinMultipler = 0.5f, // value from 0.0f to 1f where 0.1f would be a min damage of 10% of max damage.
                },
                Grids = new GridSizeDef
                {
                    Large = 1f,
                    Small = .5f,
                },
                Armor = new ArmorDef
                {
                    Armor = 1f,
                    Light = 1f,
                    Heavy = 1f,
                    NonArmor = 1f,
                },
                Shields = new ShieldDef
                {
                    Modifier = 4.0f,
                    Type = Default,
                    BypassModifier = -1f,
                },
                // first true/false (ignoreOthers) will cause projectiles to pass through all blocks that do not match the custom subtypeIds.
                Custom = new CustomScalesDef
                {
                    IgnoreAllOthers = false,
                    Types = new[]
                    {
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test1",
                            Modifier = -1f,
                        },
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test2",
                            Modifier = -1f,
                        },
                    },
                },
            },
            AreaEffect = new AreaDamageDef
            {
                AreaEffect = Disabled, // Disabled = do not use area effect at all, Explosive, Radiant, AntiSmart, JumpNullField, JumpNullField, EnergySinkField, AnchorField, EmpField, OffenseField, NavField, DotField.
                AreaEffectDamage = 0, // 0 = use spillover from BaseDamage, otherwise use this value.
                AreaEffectRadius = 0f,
                Pulse = new PulseDef // interval measured in game ticks (60 == 1 second), pulseChance chance (0 - 100) that an entity in field will be hit
                {
                    Interval = 60,
                    PulseChance = 0,
                },
                Explosions = new ExplosionDef
                {
                    NoVisuals = false,
                    NoSound = false,
                    Scale = 1,
                    CustomParticle = "",
                    CustomSound = "",
                },
                Detonation = new DetonateDef
                {
                    DetonateOnEnd = false,
                    ArmOnlyOnHit = false,
                    DetonationDamage = 0,
                    DetonationRadius = 0,
                },
                EwarFields = new EwarFieldsDef //FIX Manually
                {
                    Duration = 60,
                    StackDuration = true,
                    Depletable = false,
                    MaxStacks = 10,
                    TriggerRange = 5f,
                },
            },
            Beams = new BeamDef
            {
                Enable = false,
                VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
                ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
                RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
                OneParticle = false, // Only spawn one particle hit per beam weapon.
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = None,
                TargetLossDegree = 80f,
                TargetLossTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                MaxLifeTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                AccelPerSec = 0f,
                DesiredSpeed = 1000f,
                MaxTrajectory = 5200f,
                FieldTime = 0,				// 0 is disabled, a value causes the projectile to come to rest, spawn a field and remain for a time (Measured in game ticks, 60 = 1 second)
				GravityMultiplier = 0,
                SpeedVariance = Random(start: 0, end: 0), // subtracts value from DesiredSpeed
                RangeVariance = Random(start: 0, end: 0), // subtracts value from MaxTrajectory
                Smarts = new SmartsDef
                {
                    Inaccuracy = 1f, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
                    Aggressiveness = 1f, // controls how responsive tracking is.
                    MaxLateralThrust = 0.5, // controls how sharp the trajectile may turn
                    TrackingDelay = 0, // Measured in Shape diameter units traveled.
                    MaxChaseTime = 1000, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    OverideTarget = false, // when set to true ammo picks its own target, does not use hardpoint's.
                },
                Mines = new MinesDef
                {
                    DetectRadius = 200,
                    DeCloakRadius = 100,
                    FieldTime = 0,
                    Cloak = true,
                    Persist = false,
                },
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "",
                VisualProbability = 1f,
                ShieldHitDraw = true,
                Particles = new AmmoParticleDef
                {
                    Ammo = new ParticleDef
                    {
                        Name = "", //ShipWelderArc
                        Color = Color(red: 0, green: 200, blue: 100, alpha: 32),
                        Offset = Vector(x: 0, y: -1, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 1,
                            Scale = .5f,
                        },
                    },
                    Hit = new ParticleDef
                    {
                        Name = "PlasmaBolt",
                        ApplyToShield = true,
                        ShrinkByDistance = true,
                        Color = Color(red: 200, green: 80, blue: 80, alpha: 1),
                        Offset = Vector(x: 0, y: 0, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 1,
                            Scale = .5f,
                            HitPlayChance = 1f,
                        },
                    },
                },
                Lines = new LineDef
                {
                    TracerMaterial = "ProjectileTrailLine", // WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                    ColorVariance = Random(start: 0.75f, end: 2f), // multiply the color by random values within range.
                    WidthVariance = Random(start: 0f, end: 0.15f), // adds random value to default width (negatives shrinks width)
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = 10f,
                        Width = .5f,
                        Color = Color(red: 255, green: 204, blue: 153, alpha: 1),
                    },
                    Trail = new TrailDef
                    {
                        Enable = true,
                        Material = "WeaponLaser",
                        DecayTime = 60,
                        Color = Color(red: 100, green: 100, blue: 60, alpha: 1),
                        Back = false,
                        CustomWidth = 0.1f,
                        UseWidthVariance = false,
                        UseColorFade = true,
                    },
                    OffsetEffect = new OffsetEffectDef
                    {
                        MaxOffset = 0,// 0 offset value disables this effect
                        MinLength = 4f,
                        MaxLength = 4f,
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                TravelSound = "",
                HitSound = "",
                HitPlayChance = 1.0f,
                HitPlayShield = true,
            }, // Don't edit below this line
        };
		
		private AmmoDef TyphonBolt => new AmmoDef
        {
            AmmoMagazine = "Energy",
            AmmoRound = "TyphonBolt",
            HybridRound = false, //AmmoMagazine based weapon with energy cost
            EnergyCost = .0008f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
            BaseDamage = 30000f,
            Mass = 0f, // in kilograms
            Health = 0, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying.
            BackKickForce = 2000f,
            HardPointUsable = true, // set to false if this is a Fragment ammoType and you don't want the turret to be able to select it directly.
            EnergyMagazineSize = 1,

            Shape = new ShapeDef //defines the collision shape of projectile, defaults line and visual Line Length if set to 0
            {
                Shape = LineShape,
                Diameter = 1,
            },
            ObjectsHit = new ObjectsHitDef
            {
                MaxObjectsHit = 10, // 0 = disabled
                CountBlocks = false, // counts gridBlocks and not just entities hit
            },
            Fragment = new FragmentDef //FIX MANUALLY
            {
                AmmoRound = "",
                Fragments = 0,
                Degrees = 0,
                Reverse = false,
                RandomizeDir = false,
            },
            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0f, // 0 = disabled, 1000 = any blocks with currently integrity above 1000 will be immune to damage.
                DamageVoxels = false, // true = voxels are vulnerable to this weapon
                SelfDamage = false, // true = allow self damage.

                // modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01 = 1% damage, 2 = 200% damage.
                Characters = -1f,
				FallOff = new FallOffDef
                {
                    Distance = 6000f, // Distance at which max damage begins falling off.
                    MinMultipler = 0.5f, // value from 0.0f to 1f where 0.1f would be a min damage of 10% of max damage.
                },
                Grids = new GridSizeDef
                {
                    Large = 1f,
                    Small = .5f,
                },
                Armor = new ArmorDef
                {
                    Armor = 1f,
                    Light = 1f,
                    Heavy = 1f,
                    NonArmor = -1f,
                },
                Shields = new ShieldDef
                {
                    Modifier = 3.2f,
                    Type = Default,
                    BypassModifier = -1f,
                },
                // first true/false (ignoreOthers) will cause projectiles to pass through all blocks that do not match the custom subtypeIds.
                Custom = new CustomScalesDef
                {
                    IgnoreAllOthers = false,
                    Types = new[]
                    {
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test1",
                            Modifier = -1f,
                        },
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test2",
                            Modifier = -1f,
                        },
                    },
                },
            },
            AreaEffect = new AreaDamageDef
            {
                AreaEffect = Disabled, // Disabled = do not use area effect at all, Explosive, Radiant, AntiSmart, JumpNullField, JumpNullField, EnergySinkField, AnchorField, EmpField, OffenseField, NavField, DotField.
                AreaEffectDamage = 0, // 0 = use spillover from BaseDamage, otherwise use this value.
                AreaEffectRadius = 0f,
                Pulse = new PulseDef // interval measured in game ticks (60 == 1 second), pulseChance chance (0 - 100) that an entity in field will be hit
                {
                    Interval = 60,
                    PulseChance = 0,
                },
                Explosions = new ExplosionDef
                {
                    NoVisuals = false,
                    NoSound = false,
                    Scale = 1,
                    CustomParticle = "",
                    CustomSound = "",
                },
                Detonation = new DetonateDef
                {
                    DetonateOnEnd = false,
                    ArmOnlyOnHit = false,
                    DetonationDamage = 0,
                    DetonationRadius = 0,
                },
                EwarFields = new EwarFieldsDef //FIX Manually
                {
                    Duration = 60,
                    StackDuration = true,
                    Depletable = false,
                    MaxStacks = 10,
                    TriggerRange = 5f,
                },
            },
            Beams = new BeamDef
            {
                Enable = false,
                VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
                ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
                RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
                OneParticle = false, // Only spawn one particle hit per beam weapon.
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = None,
                TargetLossDegree = 80f,
                TargetLossTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                MaxLifeTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                AccelPerSec = 0f,
                DesiredSpeed = 1000f,
                MaxTrajectory = 5200f,
                FieldTime = 0,				// 0 is disabled, a value causes the projectile to come to rest, spawn a field and remain for a time (Measured in game ticks, 60 = 1 second)
				GravityMultiplier = 0,
                SpeedVariance = Random(start: 0, end: 0), // subtracts value from DesiredSpeed
                RangeVariance = Random(start: 0, end: 0), // subtracts value from MaxTrajectory
                Smarts = new SmartsDef
                {
                    Inaccuracy = 1f, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
                    Aggressiveness = 1f, // controls how responsive tracking is.
                    MaxLateralThrust = 0.5, // controls how sharp the trajectile may turn
                    TrackingDelay = 0, // Measured in Shape diameter units traveled.
                    MaxChaseTime = 1000, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    OverideTarget = false, // when set to true ammo picks its own target, does not use hardpoint's.
                },
                Mines = new MinesDef
                {
                    DetectRadius = 200,
                    DeCloakRadius = 100,
                    FieldTime = 0,
                    Cloak = true,
                    Persist = false,
                },
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "",
                VisualProbability = 1f,
                ShieldHitDraw = true,
                Particles = new AmmoParticleDef
                {
                    Ammo = new ParticleDef
                    {
                        Name = "", //ShipWelderArc
                        Color = Color(red: 0, green: 200, blue: 100, alpha: 32),
                        Offset = Vector(x: 0, y: -1, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 1,
                            Scale = .5f,
                        },
                    },
                    Hit = new ParticleDef
                    {
                        Name = "PlasmaBolt",
                        ApplyToShield = true,
                        ShrinkByDistance = true,
                        Color = Color(red: 200, green: 80, blue: 80, alpha: 1),
                        Offset = Vector(x: 0, y: 0, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 1,
                            Scale = .5f,
                            HitPlayChance = 1f,
                        },
                    },
                },
                Lines = new LineDef
                {
                    TracerMaterial = "ProjectileTrailLine", // WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                    ColorVariance = Random(start: 0.75f, end: 2f), // multiply the color by random values within range.
                    WidthVariance = Random(start: 0f, end: 0.15f), // adds random value to default width (negatives shrinks width)
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = 10f,
                        Width = .5f,
                        Color = Color(red: 204, green: 229, blue: 255, alpha: 1),
                    },
                    Trail = new TrailDef
                    {
                        Enable = true,
                        Material = "WeaponLaser",
                        DecayTime = 40,
                        Color = Color(red: 102, green: 178, blue: 255, alpha: 1),
                        Back = false,
                        CustomWidth = 0.1f,
                        UseWidthVariance = false,
                        UseColorFade = true,
                    },
                    OffsetEffect = new OffsetEffectDef
                    {
                        MaxOffset = 0,// 0 offset value disables this effect
                        MinLength = 4f,
                        MaxLength = 4f,
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                TravelSound = "",
                HitSound = "",
                HitPlayChance = 1.0f,
                HitPlayShield = true,
            }, // Don't edit below this line
        };
		
		private AmmoDef VXGaussRound => new AmmoDef
        {
            AmmoMagazine = "VXGaussRound",
            AmmoRound = "VXGaussRound",
            HybridRound = false, //AmmoMagazine based weapon with energy cost
            EnergyCost = .0008f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
            BaseDamage = 38000f,
            Mass = 0f, // in kilograms
            Health = 0, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying.
            BackKickForce = 4000f,
            HardPointUsable = true, // set to false if this is a Fragment ammoType and you don't want the turret to be able to select it directly.
            EnergyMagazineSize = 0,

            Shape = new ShapeDef //defines the collision shape of projectile, defaults line and visual Line Length if set to 0
            {
                Shape = LineShape,
                Diameter = 1,
            },
            ObjectsHit = new ObjectsHitDef
            {
                MaxObjectsHit = 0, // 0 = disabled
                CountBlocks = false, // counts gridBlocks and not just entities hit
            },
            Fragment = new FragmentDef //FIX MANUALLY
            {
                AmmoRound = "",
                Fragments = 0,
                Degrees = 0,
                Reverse = false,
                RandomizeDir = false,
            },
            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0f, // 0 = disabled, 1000 = any blocks with currently integrity above 1000 will be immune to damage.
                DamageVoxels = false, // true = voxels are vulnerable to this weapon
                SelfDamage = false, // true = allow self damage.

                // modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01 = 1% damage, 2 = 200% damage.
                Characters = -1f,
				FallOff = new FallOffDef
                {
                    Distance = 7000f, // Distance at which max damage begins falling off.
                    MinMultipler = 0.5f, // value from 0.0f to 1f where 0.1f would be a min damage of 10% of max damage.
                },
                Grids = new GridSizeDef
                {
                    Large = 1f,
                    Small = .5f,
                },
                Armor = new ArmorDef
                {
                    Armor = 1f,
                    Light = 1f,
                    Heavy = 1f,
                    NonArmor = 1f,
                },
                Shields = new ShieldDef
                {
                    Modifier = 4.0f,
                    Type = Default,
                    BypassModifier = -1f,
                },
                // first true/false (ignoreOthers) will cause projectiles to pass through all blocks that do not match the custom subtypeIds.
                Custom = new CustomScalesDef
                {
                    IgnoreAllOthers = false,
                    Types = new[]
                    {
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test1",
                            Modifier = -1f,
                        },
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test2",
                            Modifier = -1f,
                        },
                    },
                },
            },
            AreaEffect = new AreaDamageDef
            {
                AreaEffect = Disabled, // Disabled = do not use area effect at all, Explosive, Radiant, AntiSmart, JumpNullField, JumpNullField, EnergySinkField, AnchorField, EmpField, OffenseField, NavField, DotField.
                AreaEffectDamage = 0, // 0 = use spillover from BaseDamage, otherwise use this value.
                AreaEffectRadius = 0f,
                Pulse = new PulseDef // interval measured in game ticks (60 == 1 second), pulseChance chance (0 - 100) that an entity in field will be hit
                {
                    Interval = 60,
                    PulseChance = 0,
                },
                Explosions = new ExplosionDef
                {
                    NoVisuals = false,
                    NoSound = false,
                    Scale = 1,
                    CustomParticle = "",
                    CustomSound = "",
                },
                Detonation = new DetonateDef
                {
                    DetonateOnEnd = true,
                    ArmOnlyOnHit = false,
                    DetonationDamage = 10000,
                    DetonationRadius = 10,
                },
                EwarFields = new EwarFieldsDef //FIX Manually
                {
                    Duration = 60,
                    StackDuration = true,
                    Depletable = false,
                    MaxStacks = 10,
                    TriggerRange = 5f,
                },
            },
            Beams = new BeamDef
            {
                Enable = false,
                VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
                ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
                RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
                OneParticle = false, // Only spawn one particle hit per beam weapon.
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = None,
                TargetLossDegree = 80f,
                TargetLossTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                MaxLifeTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                AccelPerSec = 0f,
                DesiredSpeed = 2000f,
                MaxTrajectory = 5200f,
                FieldTime = 0,				// 0 is disabled, a value causes the projectile to come to rest, spawn a field and remain for a time (Measured in game ticks, 60 = 1 second)
				GravityMultiplier = 0,
                SpeedVariance = Random(start: 0, end: 0), // subtracts value from DesiredSpeed
                RangeVariance = Random(start: 0, end: 0), // subtracts value from MaxTrajectory
                Smarts = new SmartsDef
                {
                    Inaccuracy = 1f, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
                    Aggressiveness = 1f, // controls how responsive tracking is.
                    MaxLateralThrust = 0.5, // controls how sharp the trajectile may turn
                    TrackingDelay = 0, // Measured in Shape diameter units traveled.
                    MaxChaseTime = 1000, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    OverideTarget = false, // when set to true ammo picks its own target, does not use hardpoint's.
                },
                Mines = new MinesDef
                {
                    DetectRadius = 200,
                    DeCloakRadius = 100,
                    FieldTime = 0,
                    Cloak = true,
                    Persist = false,
                },
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "\\Models\\Akiad\\Small\\VXGaussRound.mwm",
                VisualProbability = 1f,
                ShieldHitDraw = true,
                Particles = new AmmoParticleDef
                {
                    Ammo = new ParticleDef
                    {
                        Name = "", //ShipWelderArc
                        Color = Color(red: 0, green: 200, blue: 100, alpha: 32),
                        Offset = Vector(x: 0, y: -1, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 1,
                            Scale = .5f,
                        },
                    },
                    Hit = new ParticleDef
                    {
                        Name = "PlasmaBolt",
                        ApplyToShield = true,
                        ShrinkByDistance = true,
                        Color = Color(red: 200, green: 80, blue: 80, alpha: 1),
                        Offset = Vector(x: 0, y: 0, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 1,
                            Scale = .5f,
                            HitPlayChance = 1f,
                        },
                    },
                },
                Lines = new LineDef
                {
                    TracerMaterial = "ProjectileTrailLine", // WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                    ColorVariance = Random(start: 0.75f, end: 2f), // multiply the color by random values within range.
                    WidthVariance = Random(start: 0f, end: 0.15f), // adds random value to default width (negatives shrinks width)
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = 6f,
                        Width = .25f,
                        Color = Color(red: 255, green: 255, blue: 255, alpha: 1),
                    },
                    Trail = new TrailDef
                    {
                        Enable = true,
                        Material = "WeaponLaser",
                        DecayTime = 20,
                        Color = Color(red: 100, green: 100, blue: 100, alpha: 1),
                        Back = false,
                        CustomWidth = 0.1f,
                        UseWidthVariance = false,
                        UseColorFade = true,
                    },
                    OffsetEffect = new OffsetEffectDef
                    {
                        MaxOffset = 0,// 0 offset value disables this effect
                        MinLength = 4f,
                        MaxLength = 4f,
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                TravelSound = "VXGaussPass",
                HitSound = "",
                HitPlayChance = 1.0f,
                HitPlayShield = true,
            }, // Don't edit below this line
        };
		
		
		private AmmoDef SmartBomb => new AmmoDef
        {
            AmmoMagazine = "SmartBomb",
                AmmoRound = "SmartBomb",
                HybridRound = false, //AmmoMagazine based weapon with energy cost
                EnergyCost = 0.00000000001f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel
                BaseDamage = 1f,
                Mass = 2f, // in kilograms
                Health = 0.3f, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying.
                BackKickForce = 2.5f,

                Shape = new ShapeDef //defines the collision shape of projectile, defaults line and visual Line Length if set to 0
                {
                    Shape = LineShape,
                    Diameter = 1,
                },
                ObjectsHit = new ObjectsHitDef
                {
                    MaxObjectsHit = 0, // 0 = disabled
                    CountBlocks = false, // counts gridBlocks and not just entities hit
                },
                Fragment = new FragmentDef
                {
                    AmmoRound = "",
                    Fragments = 0,
                    ForwardDegrees = 0,
                    BackwardDegrees = 0,
                },
                DamageScales = new DamageScaleDef
                {
                    MaxIntegrity = 0f, // 0 = disabled, 1000 = any blocks with currently integrity above 1000 will be immune to damage.
                    DamageVoxels = true, // true = voxels are vulnerable to this weapon
                    SelfDamage = false, // true = allow self damage.

                    // modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01 = 1% damage, 2 = 200% damage.
                    Characters = 0.2f,
                    Grids = new GridSizeDef
                    {
                        Large = -1f,
                        Small = 0.25f,
                    },
                    Armor = new ArmorDef
                    {
                        Armor = 0.75f,
                        Light = 0.9f,
                        Heavy = 0.75f,
                        NonArmor = 1.1f,
                    },
                    Shields = new ShieldDef
                    {
                        Modifier = .75f,
                        Type = Default,
                        BypassModifier = -1f,
                    },
                    // first true/false (ignoreOthers) will cause projectiles to pass through all blocks that do not match the custom subtypeIds.
                    Custom = new CustomScalesDef
                    {
                        IgnoreAllOthers = false,
                        Types = new []
                        {
                            new CustomBlocksDef
                            {
                                SubTypeId = "Test1",
                                Modifier = -1f,
                            },
                            new CustomBlocksDef
                            {
                                SubTypeId = "Test2",
                                Modifier = -1f,
                            },
                        },
                    },
                },
                AreaEffect = new AreaDamageDef
                {
                    AreaEffect = Explosive, // Disabled = do not use area effect at all, Explosive, Radiant, AntiSmart, JumpNullField, JumpNullField, EnergySinkField, AnchorField, EmpField, OffenseField, NavField, DotField.
                    AreaEffectDamage = 0, // 0 = use spillover from BaseDamage, otherwise use this value.
                    AreaEffectRadius = 0f,
                    Pulse = new PulseDef // interval measured in game ticks (60 == 1 second), pulseChance chance (0 - 100) that an entity in field will be hit
                    {
                        Interval = 60,
                        PulseChance = 75,
                    },
                    Explosions = new ExplosionDef
                    {
                        NoVisuals = false,
                        NoSound = false,
                        Scale = 1,
                        CustomParticle = "MyExplosionMissile_small",
                        CustomSound = "ArcWepSmallMissileExpl",
                    },
                    Detonation = new DetonateDef
                    {
                        DetonateOnEnd = true,
                        ArmOnlyOnHit = false,
                        DetonationDamage = 300,
                        DetonationRadius = 4f,
                    },
                    EwarFields = new EwarFieldsDef
                    {
                        Duration = 60,
                        StackDuration = true,
                        Depletable = false,
                        MaxStacks = 10,
                        TriggerRange = 5f,
                    },
                },
                Beams = new BeamDef
                {
                    Enable = false,
                    VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
                    ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
                    RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
                    OneParticle = false, // Only spawn one particle hit per beam weapon.
                },
                Trajectory = new TrajectoryDef
				{
					Guidance = Smart, //ammo guidance type, options are None, Smart, Remote(not used yet), TravelTo(if tracking travels to target Position, if none tracking travels to max trajectory, good for flak), DetectFixed(mines), DetectSmart(mines), DetectTravelTo(mines)
					TargetLossDegree = 180f,
					TargetLossTime = 90, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
					MaxLifeTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
					AccelPerSec = 100,
					DesiredSpeed = 1000,
					MaxTrajectory = 5000f,
					FieldTime = 0, // 0 is disabled, a value causes the projectile to come to rest, spawn a field and remain for a time (Measured in game ticks, 60 = 1 second)
					GravityMultiplier = 0f, // Gravity influences the trajectory of the projectile.
					SpeedVariance = Random(start: 0, end: 0), // subtracts value from DesiredSpeed
					RangeVariance = Random(start: 0, end: 0), // subtracts value from MaxTrajectory
					Smarts = new SmartsDef
					{
						Inaccuracy = 2f, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
						Aggressiveness = 3f, // controls how responsive tracking is.
						MaxLateralThrust = 0.5, // controls how sharp the trajectile may turn
						TrackingDelay = 200, // Measured in Shape diameter units traveled.
						MaxChaseTime = 2400, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
						OverideTarget = true, // when set to true ammo picks its own target, does not use hardpoint's.
						MaxTargets = 6,
					},
					Mines = new MinesDef
					{
						DetectRadius = 200,
						DeCloakRadius = 100,
						FieldTime = 0,
						Cloak = true,
						Persist = false,
					},
				},
                AmmoGraphics = new GraphicDef
                {
                    ModelName = "\\Models\\Akiad\\Small\\SmartBomb.mwm",
                    VisualProbability = 1f,
                    ShieldHitDraw = true,
                    Particles = new AmmoParticleDef
                    {
                        Ammo = new ParticleDef
                        {
                            Name = "Smoke_Missile", //ShipWelderArc
                            Color = Color(red: 1, green: 1, blue: 1, alpha: 1),
                            Offset = Vector(x: 0, y: 0, z: 0.3f),
                            Extras = new ParticleOptionDef
                            {
                                Loop = true,
                                Restart = false,
                                MaxDistance = 5000,
                                MaxDuration = 10,
                                Scale = 0.1f,
                            },
                        },
                        Hit = new ParticleDef
                        {
                            Name = "",
                            ApplyToShield = true,
                            ShrinkByDistance = true,
                            Color = Color(red: 1, green: 1, blue: 1, alpha: 1),
                            Offset = Vector(x: 0, y: 0, z: 0),
                            Extras = new ParticleOptionDef
                            {
                                Loop = true,
                                Restart = false,
                                MaxDistance = 5000,
                                MaxDuration = 60,
                                Scale = 0.75f,
                                HitPlayChance = 1f,
                            },
                        },
                    },
                   Lines = new LineDef
                    {
                        TracerMaterial = "ProjectileTrailLine", // Particle that follows your projectile. WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                        ColorVariance = Random(start: 5f, end: 10f), // multiply the color by random values within range.
                        WidthVariance = Random(start: 0f, end: 0.045f), // adds random value to default width (negatives shrinks width)
                        Tracer = new TracerBaseDef
                        {
                            Enable = true,
                            Length = .001f,
                            Width = 0.005f,
                            Color = Color(red: 0.9f, green: 0.9f, blue: 0.9f, alpha: 1),
                        },
                        Trail = new TrailDef
                        {
                            Enable = true,
                            Material = "WeaponLaser",
                            DecayTime = 3,
                            Color = Color(red: 50, green: 10, blue: 10, alpha: 1),
                            Back = true,
                            CustomWidth = 0,
                            UseWidthVariance = false,
                            UseColorFade = true,
                        },
                        OffsetEffect = new OffsetEffectDef
                        {
                            MaxOffset = 0,// 0 offset value disables this effect
                            MinLength = 0.2f,
                            MaxLength = 3,
                        },
                    },
                },
                AmmoAudio = new AmmoAudioDef
                {
                    TravelSound = "",//MissileLoop
                    HitSound = "",
                    HitPlayChance = 1.0f,
                    HitPlayShield = true,
                }, // Don't edit below this line
            
            
        };
		
		private AmmoDef DesignatorLaser => new AmmoDef
        {
            AmmoMagazine = "Energy",
            AmmoRound = "DesignatorLaser",
            HybridRound = false, //AmmoMagazine based weapon with energy cost
            EnergyCost = 1.5f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel 1.25f
            BaseDamage = 0,
            Mass = 0.0f, // in kilograms
            Health = 0, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying.
            BackKickForce = 0f,
            HardPointUsable = true, // set to false if this is a Fragment ammoType and you don't want the turret to be able to select it directly.
            EnergyMagazineSize = 0,

            Shape = new ShapeDef //defines the collision shape of projectile, defaults line and visual Line Length if set to 0
            {
                Shape = LineShape,
                Diameter = 1,
            },
            ObjectsHit = new ObjectsHitDef
            {
                MaxObjectsHit = 2, // 0 = disabled
                CountBlocks = false, // counts gridBlocks and not just entities hit
            },
            Fragment = new FragmentDef //FIX MANUALLY
            {
                AmmoRound = "",
                Fragments = 0,
                Degrees = 0,
                Reverse = false,
                RandomizeDir = false,
            },
            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0f, // 0 = disabled, 1000 = any blocks with currently integrity above 1000 will be immune to damage.
                DamageVoxels = false, // true = voxels are vulnerable to this weapon
                SelfDamage = false, // true = allow self damage.

                // modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01 = 1% damage, 2 = 200% damage.
                Characters = -1f,
                FallOff = new FallOffDef
                {
                    Distance = 1500f, // Distance at which max damage begins falling off.
                    MinMultipler = 0.5f, // value from 0.0f to 1f where 0.1f would be a min damage of 10% of max damage.
                },
                Grids = new GridSizeDef
                {
                    Large = -1f,
                    Small = 0.25f,
                },
                Armor = new ArmorDef
                {
                    Armor = 0.8f,
                    Light = -1f,
                    Heavy = .8f,
                    NonArmor = .9f,
                },
                Shields = new ShieldDef
                {
                    Modifier = 1.0f,
                    Type = Bypass,
                    BypassModifier = 1.0f,
                },
                // first true/false (ignoreOthers) will cause projectiles to pass through all blocks that do not match the custom subtypeIds.
                Custom = new CustomScalesDef
                {
                    IgnoreAllOthers = false,
                    Types = new[]
                    {
                        new CustomBlocksDef
                        {
                            SubTypeId = "LargeHeavyBlockArmorBlock",
                            Modifier = 2f,
                        },
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test2",
                            Modifier = -1f,
                        },
                    },
                },
            },
            AreaEffect = new AreaDamageDef
            {
                AreaEffect = Disabled, // Disabled = do not use area effect at all, Explosive, Radiant, AntiSmart, JumpNullField, JumpNullField, EnergySinkField, AnchorField, EmpField, OffenseField, NavField, DotField.
                AreaEffectDamage = 10f, // 0 = use spillover from BaseDamage, otherwise use this value.
                AreaEffectRadius = 1.5f,
                Pulse = new PulseDef // interval measured in game ticks (60 == 1 second), pulseChance chance (0 - 100) that an entity in field will be hit
                {
                    Interval = 60,
                    PulseChance = 60,
                },
                Explosions = new ExplosionDef
                {
                    NoVisuals = false,
                    NoSound = false,
                    Scale = 1,
                    CustomParticle = "",
                    CustomSound = "",
                },
                Detonation = new DetonateDef
                {
                    DetonateOnEnd = false,
                    ArmOnlyOnHit = false,
                    DetonationDamage = 0,
                    DetonationRadius = 0,
                },
                EwarFields = new EwarFieldsDef //FIX Manually
                {
                    Duration = 60,
                    StackDuration = true,
                    Depletable = false,
                    MaxStacks = 10,
                    TriggerRange = 5f,
                },
            },
            Beams = new BeamDef
            {
                Enable = true,
                VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
                ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
                RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
                OneParticle = false, // Only spawn one particle hit per beam weapon.
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = None,
                TargetLossDegree = 80f,
                TargetLossTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                MaxLifeTime = 900, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                AccelPerSec = 0f,
                DesiredSpeed = 0f,
                MaxTrajectory = 15000f,
                FieldTime = 0, // 0 is disabled, a value causes the projectile to come to rest, spawn a field and remain for a time (Measured in game ticks, 60 = 1 second)
                SpeedVariance = Random(start: 0, end: 0), // subtracts value from DesiredSpeed
                RangeVariance = Random(start: 0, end: 0), // subtracts value from MaxTrajectory
                Smarts = new SmartsDef
                {
                    Inaccuracy = 0f, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
                    Aggressiveness = 1f, // controls how responsive tracking is.
                    MaxLateralThrust = 0.5, // controls how sharp the trajectile may turn
                    TrackingDelay = 1, // Measured in Shape diameter units traveled.
                    MaxChaseTime = 1800, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    OverideTarget = false, // when set to true ammo picks its own target, does not use hardpoint's.
                },
                Mines = new MinesDef
                {
                    DetectRadius = 200,
                    DeCloakRadius = 100,
                    FieldTime = 0,
                    Cloak = true,
                    Persist = false,
                },
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "",
                VisualProbability = 1f,
                ShieldHitDraw = true,
                Particles = new AmmoParticleDef
                {
                    Ammo = new ParticleDef
                    {
                        Name = "", //ShipWelderArc
                        Color = Color(red: 128, green: 0, blue: 0, alpha: 32),
                        Offset = Vector(x: 0, y: -1, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 1,
                            Scale = 1,
                        },
                    },
                    Hit = new ParticleDef
                    {
                        Name = "",
                        ApplyToShield = true,
                        ShrinkByDistance = true,
                        Color = Color(red: 200, green: 40, blue: 0, alpha: 1),
                        Offset = Vector(x: 0, y: 0, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 1,
                            Scale = 0.1f,
                            HitPlayChance = 1f,
                        },
                    },
                },
                Lines = new LineDef
                {
                    TracerMaterial = "WeaponLaser", // WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                    ColorVariance = Random(start: 0f, end: 0f), // multiply the color by random values within range.
                    WidthVariance = Random(start: 0f, end: 0f), // adds random value to default width (negatives shrinks width)
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = 1f,
                        Width = 0.01f,
                        Color = Color(red: 20, green: 0, blue: 0, alpha: 1),
                        VisualFadeStart = 0, // Number of ticks the weapon has been firing before projectiles begin to fade their color
                        VisualFadeEnd = 0, // How many ticks after fade began before it will be invisible.
                    },
                    Trail = new TrailDef
                    {
                        Enable = false,
                        Material = "",
                        DecayTime = 10,
                        Color = Color(red: 10, green: 0, blue: 0, alpha: 1),
                        Back = false,
                        CustomWidth = 0.10f,
                        UseWidthVariance = true,
                        UseColorFade = true,
                    },
                    OffsetEffect = new OffsetEffectDef
                    {
                        MaxOffset = 0f,// 0 offset value disables this effect 0.15f
                        MinLength = 5f,
                        MaxLength = 10f,
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                TravelSound = "",
                HitSound = "",
                HitPlayChance = 1.0f,
                HitPlayShield = true,
            }, // Don't edit below this line
        };
		
		
		
		private AmmoDef SkybreakerLaser => new AmmoDef
        {
            AmmoMagazine = "Energy",
            AmmoRound = "SkybreakerLaser",
            HybridRound = false, //AmmoMagazine based weapon with energy cost
            EnergyCost = 0.25f, //(((EnergyCost * DefaultDamage) * ShotsPerSecond) * BarrelsPerShot) * ShotsPerBarrel 1.25f
            BaseDamage = 2000,
            Mass = 0.0f, // in kilograms
            Health = 0, // 0 = disabled, otherwise how much damage it can take from other trajectiles before dying.
            BackKickForce = 0f,
            HardPointUsable = true, // set to false if this is a Fragment ammoType and you don't want the turret to be able to select it directly.
            EnergyMagazineSize = 0,

            Shape = new ShapeDef //defines the collision shape of projectile, defaults line and visual Line Length if set to 0
            {
                Shape = LineShape,
                Diameter = 5,
            },
            ObjectsHit = new ObjectsHitDef
            {
                MaxObjectsHit = 0, // 0 = disabled
                CountBlocks = false, // counts gridBlocks and not just entities hit
            },
            Fragment = new FragmentDef //FIX MANUALLY
            {
                AmmoRound = "",
                Fragments = 0,
                Degrees = 0,
                Reverse = false,
                RandomizeDir = false,
            },
            DamageScales = new DamageScaleDef
            {
                MaxIntegrity = 0f, // 0 = disabled, 1000 = any blocks with currently integrity above 1000 will be immune to damage.
                DamageVoxels = true, // true = voxels are vulnerable to this weapon
                SelfDamage = false, // true = allow self damage.
				VoxelHitModifier = .35,

                // modifier values: -1 = disabled (higher performance), 0 = no damage, 0.01 = 1% damage, 2 = 200% damage.
                Characters = -1f,
                FallOff = new FallOffDef
                {
                    Distance = 35000f, // Distance at which max damage begins falling off.
                    MinMultipler = 0.5f, // value from 0.0f to 1f where 0.1f would be a min damage of 10% of max damage.
                },
                Grids = new GridSizeDef
                {
                    Large = -1f,
                    Small = -1f,
                },
                Armor = new ArmorDef
                {
                    Armor = -1f,
                    Light = -1f,
                    Heavy = -1f,
                    NonArmor = -1f,
                },
                Shields = new ShieldDef
                {
                    Modifier = .25f,
                    Type = Default,
                    BypassModifier = 1.0f,
                },
                // first true/false (ignoreOthers) will cause projectiles to pass through all blocks that do not match the custom subtypeIds.
                Custom = new CustomScalesDef
                {
                    IgnoreAllOthers = false,
                    Types = new[]
                    {
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test1",
                            Modifier = -1f,
                        },
                        new CustomBlocksDef
                        {
                            SubTypeId = "Test2",
                            Modifier = -1f,
                        },
                    },
                },
            },
            AreaEffect = new AreaDamageDef
            {
                AreaEffect = Radiant, // Disabled = do not use area effect at all, Explosive, Radiant, AntiSmart, JumpNullField, JumpNullField, EnergySinkField, AnchorField, EmpField, OffenseField, NavField, DotField.
                AreaEffectDamage = 8000f, // 0 = use spillover from BaseDamage, otherwise use this value.
                AreaEffectRadius = 10f,
                Explosions = new ExplosionDef
                {
                    NoVisuals = false,
                    NoSound = false,
                    Scale = 1,
                    CustomParticle = "",
                    CustomSound = "",
                },
                Detonation = new DetonateDef
                {
                    DetonateOnEnd = false,
                    ArmOnlyOnHit = false,
                    DetonationDamage = 8000,
                    DetonationRadius = 10,
                },
                EwarFields = new EwarFieldsDef //FIX Manually
                {
                    Duration = 60,
                    StackDuration = true,
                    Depletable = false,
                    MaxStacks = 10,
                    TriggerRange = 5f,
                },
            },
            Beams = new BeamDef
            {
                Enable = true,
                VirtualBeams = false, // Only one hot beam, but with the effectiveness of the virtual beams combined (better performace)
                ConvergeBeams = false, // When using virtual beams this option visually converges the beams to the location of the real beam.
                RotateRealBeam = false, // The real (hot beam) is rotated between all virtual beams, instead of centered between them.
                OneParticle = false, // Only spawn one particle hit per beam weapon.
            },
            Trajectory = new TrajectoryDef
            {
                Guidance = None,
                TargetLossDegree = 80f,
                TargetLossTime = 0, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                MaxLifeTime = 900, // 0 is disabled, Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                AccelPerSec = 0f,
                DesiredSpeed = 0f,
                MaxTrajectory = 6000f,
                FieldTime = 0, // 0 is disabled, a value causes the projectile to come to rest, spawn a field and remain for a time (Measured in game ticks, 60 = 1 second)
                SpeedVariance = Random(start: 0, end: 0), // subtracts value from DesiredSpeed
                RangeVariance = Random(start: 0, end: 0), // subtracts value from MaxTrajectory
                Smarts = new SmartsDef
                {
                    Inaccuracy = 0f, // 0 is perfect, hit accuracy will be a random num of meters between 0 and this value.
                    Aggressiveness = 1f, // controls how responsive tracking is.
                    MaxLateralThrust = 0.5, // controls how sharp the trajectile may turn
                    TrackingDelay = 1, // Measured in Shape diameter units traveled.
                    MaxChaseTime = 1800, // Measured in game ticks (6 = 100ms, 60 = 1 seconds, etc..).
                    OverideTarget = false, // when set to true ammo picks its own target, does not use hardpoint's.
                },
                Mines = new MinesDef
                {
                    DetectRadius = 200,
                    DeCloakRadius = 100,
                    FieldTime = 0,
                    Cloak = true,
                    Persist = false,
                },
            },
            AmmoGraphics = new GraphicDef
            {
                ModelName = "",
                VisualProbability = 1f,
                ShieldHitDraw = true,
                Particles = new AmmoParticleDef
                {
                    Ammo = new ParticleDef
                    {
                        Name = "ArbalestBlast", //ShipWelderArc
                        Color = Color(red: 128, green: 120, blue: 128, alpha: 32),
                        Offset = Vector(x: 0, y: -1, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = true,
                            Restart = false,
                            MaxDistance = 10000,
                            MaxDuration = 1,
                            Scale = 6,
                        },
                    },
                    Hit = new ParticleDef
                    {
                        Name = "ArbalestBlast",
                        ApplyToShield = true,
                        ShrinkByDistance = true,
                        Color = Color(red: 200, green: 150, blue: 200, alpha: 1),
                        Offset = Vector(x: 0, y: 0, z: 0),
                        Extras = new ParticleOptionDef
                        {
                            Loop = false,
                            Restart = false,
                            MaxDistance = 5000,
                            MaxDuration = 1,
                            Scale = 0.1f,
                            HitPlayChance = 1f,
                        },
                    },
                },
                Lines = new LineDef
                {
                    TracerMaterial = "WeaponLaser", // WeaponLaser, ProjectileTrailLine, WarpBubble, etc..
                    ColorVariance = Random(start: 0f, end: 0f), // multiply the color by random values within range.
                    WidthVariance = Random(start: 0f, end: 1f), // adds random value to default width (negatives shrinks width)
                    Tracer = new TracerBaseDef
                    {
                        Enable = true,
                        Length = 1f,
                        Width = 4.5f,
                        Color = Color(red: 50, green: 20, blue: 30, alpha: 1),
                        VisualFadeStart = 0, // Number of ticks the weapon has been firing before projectiles begin to fade their color
                        VisualFadeEnd = 0, // How many ticks after fade began before it will be invisible.
                    },
                    Trail = new TrailDef
                    {
                        Enable = false,
                        Material = "",
                        DecayTime = 10,
                        Color = Color(red: 10, green: 0, blue: 0, alpha: 1),
                        Back = false,
                        CustomWidth = 0.10f,
                        UseWidthVariance = true,
                        UseColorFade = true,
                    },
                    OffsetEffect = new OffsetEffectDef
                    {
                        MaxOffset = 0f,// 0 offset value disables this effect 0.15f
                        MinLength = 5f,
                        MaxLength = 10f,
                    },
                },
            },
            AmmoAudio = new AmmoAudioDef
            {
                TravelSound = "",
                HitSound = "",
                HitPlayChance = 1.0f,
                HitPlayShield = true,
            }, // Don't edit below this line
        };
		
    }
}