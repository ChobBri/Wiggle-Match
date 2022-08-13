using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ScoreData
{
    public char[] initials;
    public int totalSeconds;
    public int highestLevel;

    public ScoreData(char[] initials, int totalSeconds, int highestLevel)
    {
        this.initials = initials;
        this.totalSeconds = totalSeconds;
        this.highestLevel = highestLevel;
    }

    public override bool Equals(object obj)
    {
        if (obj is null)
        {
            return false;
        }

        ScoreData data = (ScoreData) obj;

        // Optimization for a common success case.
        if (Object.ReferenceEquals(this, obj))
        {
            return true;
        }

        // If run-time types are not exactly the same, return false.
        if (this.GetType() != obj.GetType())
        {
            return false;
        }

        // Return true if the fields match.
        // Note that the base class is not invoked because it is
        // System.Object, which defines Equals as reference equality.
        return (System.Linq.Enumerable.SequenceEqual<char>(initials, data.initials)) && (totalSeconds == data.totalSeconds) && (highestLevel == data.highestLevel);
    }

    public override int GetHashCode()
    {
        return System.HashCode.Combine(initials, totalSeconds, highestLevel);
    }

    public static bool operator ==(ScoreData lhs, ScoreData rhs)
    {
        // Equals handles case of null on right side.
        return lhs.Equals(rhs);
    }

    public static bool operator !=(ScoreData lhs, ScoreData rhs) => !(lhs == rhs);
}
