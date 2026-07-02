
using System;
using Terraria;
using Terraria.ID;
using TerrariaApi.Server;
using TShockAPI;

namespace permaEffects;

[ApiVersion(2, 1)]
public class PermaEffectsPlugin(Main game) : TerrariaPlugin(game) {
    public override string Name => "PermaEffectsPlugin";
    public override string Author => "sergwest";
    public override string Description => "give permament effects to player with 30 or more potions";
    public override Version Version => new(1, 1, 0);
    public override void Initialize() {
        TShockAPI.GetDataHandlers.PlayerSpawn += OnPlayerSpawn;
    }

    protected override void Dispose(bool disposing) {
        if (disposing) {
            TShockAPI.GetDataHandlers.PlayerSpawn -= OnPlayerSpawn;
        }

        base.Dispose(disposing);
    }

    static void checkInventoryForBuffs(TSPlayer player, Item[] items) {
        foreach (Item item in items) {
            if (item.buffType != 0) {
                if (item.stack >= 30) player.SetBuff(item.buffType, Int32.MaxValue, true);
            } else {
                var buffType = item.type switch {
                    ItemID.AmmoBox => BuffID.AmmoBox,
                    ItemID.BewitchingTable => BuffID.Bewitched,
                    ItemID.SharpeningStation => BuffID.Sharpened,
                    ItemID.WarTable => BuffID.WarTable,
                    ItemID.CrystalBall => BuffID.Clairvoyance,
                    ItemID.DeadCellsPotionStation => BuffID.DeadCellsPotionStation,
                    ItemID.CatBast => BuffID.CatBast,
                    ItemID.SliceOfCake => BuffID.SugarRush,
                    ItemID.HeartLantern => BuffID.HeartLamp,
                    ItemID.StarinaBottle => BuffID.StarInBottle,
                    ItemID.Sunflower => BuffID.Sunflower,
                    // TODO: maybe honey bottles and all candle types?
                    ItemID.Campfire or 
                    (>= ItemID.CursedCampfire      and <= ItemID.RainbowCampfire) or
                    (>= ItemID.UltraBrightCampfire and <= ItemID.BoneCampfire   ) or
                    (>= ItemID.DesertCampfire      and <= ItemID.JungleCampfire )
                        => BuffID.Campfire,  // TODO: please, say, tshock has some sort of IsisCampfire(id)!!!

                    _ => 0
                };
                if ((buffType != 0) && (item.stack >= 3)) {
                    player.SetBuff(buffType, Int32.MaxValue, true);
                }
            }
        }
    }

    void OnPlayerSpawn(object sender, TShockAPI.GetDataHandlers.SpawnEventArgs args) {
        if (args.Handled) return;

        var player = args.Player;
        var pl = player.TPlayer;

        checkInventoryForBuffs(player, pl.bank.item);
        checkInventoryForBuffs(player, pl.bank2.item);
        checkInventoryForBuffs(player, pl.bank3.item);
        checkInventoryForBuffs(player, pl.bank4.item);
    }
}
