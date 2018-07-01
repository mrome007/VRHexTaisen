using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class TaisenParty : IEnumerable<TaisenUnitTurn>
{
    public bool Dead { get { return unitTurns.Count > 0 && unitTurns.All(unit => unit.Unit.Health <= 0); } }

    [SerializeField]
    private List<TaisenUnitTurn> unitTurns;

    public IEnumerator<TaisenUnitTurn> GetEnumerator() { return unitTurns.GetEnumerator(); }
    IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
}
