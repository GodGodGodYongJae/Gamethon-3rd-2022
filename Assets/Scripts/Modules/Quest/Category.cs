using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Target���� �� �ؾ� ����Ƚ���� ���� �� �ִ����� ���� ������ �����Ƿ� �з��� �������� ������� ��� Ŭ����

[CreateAssetMenu(menuName ="Category",fileName ="Category_")]
public class Category : ScriptableObject,IEquatable<Category>
{

    [SerializeField]
    private string codeName;
    [SerializeField]
    private string displayName;

    public string CodeName => codeName;
    public string DisplayName => displayName;
    // ���� ī�װ��� Code Name�� ���ϴ� ��찡 ���� IEquatable ���

    #region Operator
    public bool Equals(Category other)
    {
        //http://daplus.net/c-c-equals-referenceequals-%EB%B0%8F-%EC%97%B0%EC%82%B0%EC%9E%90/
        if (other is null)
            return false;
        if (ReferenceEquals(other, this))
            return true;

        if (GetType() != other.GetType())
            return false;

        return codeName == other.codeName;
    }

    public override bool Equals(object other)
    {
        return base.Equals(other);
    }
    public override int GetHashCode() => (CodeName, DisplayName).GetHashCode();

    public static bool operator ==(Category lhs, string rhs)
    {
        if(lhs is null)
            return ReferenceEquals(rhs, null);
        return lhs.CodeName == rhs || lhs.DisplayName == rhs;
    }

    public static bool operator !=(Category lhs, string rhs) => !(lhs == rhs);
    //categroy.CodeName == "Kill" X
    // catrgory == "Kill"
    #endregion


}
