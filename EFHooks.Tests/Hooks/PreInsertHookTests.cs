﻿using System;
using System.Data;
using NUnit.Framework;

namespace EFHooks.Tests.Hooks
{
    public class PreInsertHookTests
    {
        private class TimestampPreInsertHook : PreInsertHook<ITimeStamped>
        {
            public override void Hook(ITimeStamped entity, HookEntityMetadata metadata)
            {
                entity.CreatedAt = DateTime.Now;
            }
        }

        [Test]
        public void PreInsertHook_HasAddedHookState()
        {
            var hook = new TimestampPreInsertHook();
            Assert.AreEqual(EntityState.Added, hook.HookStates);
        }

        [Test]
        public void PreInsertHook_InterfaceHookCallsIntoGenericMethod()
        {
            var hook = new TimestampPreInsertHook();
            var entity = new TimestampedSoftDeletedEntity();

            ((IHook) hook).Hook(entity, null);
            Assert.AreEqual(DateTime.Today, entity.CreatedAt.Date);
        }
    }
}