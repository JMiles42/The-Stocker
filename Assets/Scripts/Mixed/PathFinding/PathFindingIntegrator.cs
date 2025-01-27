﻿using System.Threading.Tasks;
using ForestOfChaosLib.Types;

public static class PathFindingIntegrator
{
	public static async Task<TilePath> GetPathAsync(Vector2I start, Vector2I end, Map map)
	{
		return await Task<TilePath>.Factory.StartNew(() => GetPath(start, end, map));
	}

	public static TilePath GetPath(Vector2I start, Vector2I end, Map map)
	{
		return new TilePath {start, end};
	}
}