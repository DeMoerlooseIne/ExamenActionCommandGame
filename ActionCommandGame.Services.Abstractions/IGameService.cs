﻿using ActionCommandGame.Services.Model.Core;
using ActionCommandGame.Services.Model.Results;
using System.Threading.Tasks;

namespace ActionCommandGame.Services.Abstractions;

public interface IGameService
{
    Task<ServiceResult<GameResult>> PerformActionAsync(int playerId, string authenticatedUserId);
    Task<ServiceResult<BuyResult>> BuyAsync(int playerId, int itemId, string authenticatedUserId);
}