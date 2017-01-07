﻿using System.Windows.Input;
using Ensage.Common.Menu;

namespace Zaio
{
    public enum TargetSelectorMode
    {
        NearestToMouse,
        Auto
    }

    public class ZaioMenu
    {
        private static Menu _menu;

        private static Menu _heroSettings;

        private static MenuItem _comboKey;
        private static MenuItem _orbwalker;
        private static MenuItem _targetSelector;
        private static MenuItem _displayAttackRange;
        private static MenuItem _lockTarget;
        private static MenuItem _killSteal;

        public static Key ComboKey => KeyInterop.KeyFromVirtualKey((int) _comboKey.GetValue<KeyBind>().Key);
        public static bool ShouldUseOrbwalker => _orbwalker.GetValue<bool>();
        public static bool ShouldDisplayAttackRange => _displayAttackRange.GetValue<bool>();
        public static bool ShouldLockTarget => _lockTarget.GetValue<bool>();
        public static bool ShouldKillSteal => _killSteal.GetValue<bool>();

        public static void OnLoad()
        {
            if (_menu != null)
            {
                return;
            }

            _menu = new Menu("Zaio", "zaioMenu", true);

            //  ###########
            var general = new Menu("General", "zaioGeneral");

            _orbwalker = new MenuItem("zaioOrbwalk", "Orbwalk").SetValue(true);
            general.AddItem(_orbwalker);

            _displayAttackRange = new MenuItem("zaioAttackRange", "Display AttackRange").SetValue(true);
            general.AddItem(_displayAttackRange);

            _lockTarget = new MenuItem("zaioLockTarget", "Lock Combo-Target").SetValue(true);
            general.AddItem(_lockTarget);

            _killSteal = new MenuItem("zaioKillSteal", "Auto KillSteal").SetValue(true);
            general.AddItem(_killSteal);

            _targetSelector =
                new MenuItem("zaioTargetSelector", "Target Selector").SetValue(new[] {"Closest to mouse", "Auto"});
            general.AddItem(_targetSelector);

            _menu.AddSubMenu(general);

            // ###########
            var hotkeys = new Menu("Hotkeys", "zaioHotkeys");

            _comboKey = new MenuItem("zaioCombo", "Combo").SetValue(new KeyBind(0, KeyBindType.Press));
            hotkeys.AddItem(_comboKey);

            _menu.AddSubMenu(hotkeys);

            //  ###########
            _menu.AddToMainMenu();
        }

        public static void LoadHeroSettings(Menu newHeroSettings)
        {
            ResetHeroSettings();

            _heroSettings = newHeroSettings;
            _menu.AddSubMenu(_heroSettings);
        }

        public static void ResetHeroSettings()
        {
            if (_heroSettings != null)
            {
                _menu.RemoveSubMenu(_heroSettings.Name);
                _heroSettings = null;
            }
        }
    }
}