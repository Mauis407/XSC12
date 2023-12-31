﻿using System.Linq;
using System.Numerics;
using Dalamud.Game.ClientState.JobGauge.Types;
using Dalamud.Game.ClientState.Objects.SubKinds;
using Dalamud.Game.ClientState.Objects.Types;
using Dalamud.Game.ClientState.Statuses;
using ImGuiNET;
using XIVSlothCombo.Combos;
using XIVSlothCombo.Combos.PvE;
using XIVSlothCombo.CustomComboNS;
using XIVSlothCombo.CustomComboNS.Functions;
using XIVSlothCombo.Data;
using XIVSlothCombo.Extensions;
using XIVSlothCombo.Services;

#if DEBUG
namespace XIVSlothCombo.Window.Tabs
{

    internal class Debug : ConfigWindow
    {

        internal class DebugCombo : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; }

            protected override uint Invoke(uint actionID, uint lastComboActionID, float comboTime, byte level) => actionID;
        }

        internal static new void Draw()
        {
            PlayerCharacter? LocalPlayer = Service.ClientState.LocalPlayer;
            DebugCombo? comboClass = new();

            if (LocalPlayer != null)
            {
                if (Service.ClientState.LocalPlayer.TargetObject is BattleChara chara)
                {
                    foreach (Status? status in chara.StatusList)
                    {
                        ImGui.TextUnformatted($"TARGET STATUS CHECK: {chara.Name} -> {ActionWatching.GetStatusName(status.StatusId)}: {status.StatusId}");
                    }
                }

                foreach (Status? status in (Service.ClientState.LocalPlayer as BattleChara).StatusList)
                {
                    ImGui.TextUnformatted($"SELF STATUS CHECK: {Service.ClientState.LocalPlayer.Name} -> {ActionWatching.GetStatusName(status.StatusId)}: {status.StatusId}");
                }

                ImGui.TextUnformatted($"TARGET OBJECT KIND: {Service.ClientState.LocalPlayer.TargetObject?.ObjectKind}");
                ImGui.TextUnformatted($"TARGET IS BATTLE CHARA: {Service.ClientState.LocalPlayer.TargetObject is BattleChara}");
                ImGui.TextUnformatted($"PLAYER IS BATTLE CHARA: {LocalPlayer is BattleChara}");
                ImGui.TextUnformatted($"IN COMBAT: {CustomComboFunctions.InCombat()}");
                ImGui.TextUnformatted($"IN MELEE RANGE: {CustomComboFunctions.InMeleeRange()}");
                ImGui.TextUnformatted($"DISTANCE FROM TARGET: {CustomComboFunctions.GetTargetDistance()}");
                ImGui.TextUnformatted($"TARGET HP VALUE: {CustomComboFunctions.EnemyHealthCurrentHp()}");
                ImGui.TextUnformatted($"LAST ACTION: {ActionWatching.GetActionName(ActionWatching.LastAction)} (ID:{ActionWatching.LastAction})");
                ImGui.TextUnformatted($"LAST ACTION COST: {CustomComboFunctions.GetResourceCost(ActionWatching.LastAction)}");
                ImGui.TextUnformatted($"LAST ACTION TYPE: {ActionWatching.GetAttackType(ActionWatching.LastAction)}");
                ImGui.TextUnformatted($"LAST WEAPONSKILL: {ActionWatching.GetActionName(ActionWatching.LastWeaponskill)}");
                ImGui.TextUnformatted($"LAST SPELL: {ActionWatching.GetActionName(ActionWatching.LastSpell)}");
                ImGui.TextUnformatted($"LAST ABILITY: {ActionWatching.GetActionName(ActionWatching.LastAbility)}");
                ImGui.TextUnformatted($"ZONE: {Service.ClientState.TerritoryType}");
                
                ImGui.TextUnformatted($"buff : {CustomComboFunctions.GetBuffRemainingTime(PLD.Buffs.DivineMight)}");
                ImGui.TextUnformatted($"王权层数 : {CustomComboFunctions.GetBuffStacks(PLD.Buffs.SwordOath)}");
                ImGui.TextUnformatted($"战逃倒计时 : {CustomComboFunctions.GetCooldownRemainingTime(PLD.FightOrFlight)}");
                
                ImGui.TextUnformatted($"战逃个数 : {ActionWatching.CombatActions.FindAll(actionId => actionId ==PLD. FightOrFlight).Count}");
                
                ImGui.TextUnformatted($"摆烂循环 : {PLD.ActionLoop}");
                ImGui.TextUnformatted($"摆烂循环 : {CustomComboFunctions.GetOptionValue(PLD.Config.PLD_FOF_GCD)}");
                
                ImGui.TextUnformatted($"WasLast2ActionsAbilities : {ActionWatching.WasLast2ActionsAbilities()}");
                if (ActionWatching.CombatActions.Count > 2)
                {
                    ImGui.TextUnformatted($"摆烂循环1 : {ActionWatching.CombatActions.Last()}");
                    ImGui.TextUnformatted($"摆烂循环2 : {ActionWatching.CombatActions[ActionWatching.CombatActions.Count - 2]}");
                    ImGui.TextUnformatted($"摆烂循环3 : {ActionWatching.GetAttackType(ActionWatching.CombatActions.Last())}");
                }


                // ImGui.TextUnformatted($"战逃个数 : {ActionWatching.CombatActions.Count}");
                
                
                ImGui.BeginChild("BLUSPELLS", new Vector2(250, 100), false);
                ImGui.TextUnformatted($"SELECTED BLU SPELLS:\n{string.Join("\n", Service.Configuration.ActiveBLUSpells.Select(x => ActionWatching.GetActionName(x)).OrderBy(x => x))}");
                ImGui.EndChild();
                ImGui.TextUnformatted($"{CustomComboFunctions.GetJobGauge<PLDGauge>().OathGauge >= CustomComboFunctions.GetOptionValue(PLD.Config.PLD_SheltronOption)}");

            }

            else
            {
                ImGui.TextUnformatted("Please log in to use this tab.");
            }
        }
    }
}
#endif