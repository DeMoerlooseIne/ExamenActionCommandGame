﻿using ActionCommandGame.Abstractions;

namespace ActionCommandGame.Services.Model.Results;

public class NegativeGameEventResult : IHasProbability
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string DefenseWithArmorDescription { get; set; }
    public string DefenseWithoutArmorDescription { get; set; }
    public int DefenseLoss { get; set; }
    public int Probability { get; set; }
}