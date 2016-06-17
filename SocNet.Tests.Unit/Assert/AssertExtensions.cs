using System.Collections.Generic;
using FluentAssertions;

namespace SocNet.Tests.Unit.Assert
{
    static class AssertExtensions
    {
        public static void ShouldInclude<TKey, TValue>(this IDictionary<TKey, TValue> obj, TKey key)
        {
            obj.Should().ContainKey(key);
        }

        public static TValue For<TKey, TValue>(this IDictionary<TKey, TValue> obj, TKey key)
        {
            return obj[key];
        }
    }
}