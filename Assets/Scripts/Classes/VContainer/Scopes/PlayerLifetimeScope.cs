using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class PlayerLifetimeScope : LifetimeScope
{
    [SerializeField] private PlayerController player; 
    protected override void Configure(IContainerBuilder builder)
    {
        Debug.Log("Player Scope");

        builder.RegisterComponent(player);
        builder.RegisterEntryPoint<PlayerEntryPoint>();
    }
}
