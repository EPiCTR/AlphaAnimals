﻿using RimWorld;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Verse;

namespace AlphaBehavioursAndEvents
{
    public class CompDigWhenHungry : ThingComp
    {
        public int stopdiggingcounter = 0;
        private Effecter effecter;


        public CompProperties_DigWhenHungry Props
        {
            get
            {
                return (CompProperties_DigWhenHungry)this.props;
            }
        }


        protected string customThingToDig
        {
            get
            {
                return this.Props.customThingToDig;
            }
        }

        public override void CompTick()
        {
            base.CompTick();
            Pawn pawn = this.parent as Pawn;
            if ((pawn.Map != null)&&(pawn.needs.food.CurLevelPercentage < pawn.needs.food.PercentageThreshHungry)&&(pawn.Awake()))
            {
               
                if (stopdiggingcounter <= 0) {

                    PawnKindDef wildman = PawnKindDef.Named("WildMan");
                    Faction faction = FactionUtility.DefaultFactionFrom(wildman.defaultFactionType);
                    Pawn newPawn = PawnGenerator.GeneratePawn(wildman, faction);
                    newPawn.health.SetDead();
                    GenSpawn.Spawn(newPawn, pawn.Position, pawn.Map, WipeMode.Vanish);
                    if (this.effecter == null)
					{
                        this.effecter = EffecterDefOf.Mine.Spawn();
                    }
                    this.effecter.Trigger(pawn, newPawn);
                    stopdiggingcounter = 40000;
                }
                stopdiggingcounter--;

            }
        }

     
    }
}
