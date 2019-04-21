using System;
using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class SqliteDataBase
{
#if UNITY_IPHONE
#if UNITY_EDITOR
        const string LIBNAME = "sqlite_unity_plugin";
#else
        const string LIBNAME = "__Internal";
#endif
#else
    const string LIBNAME = "sqlite_unity_plugin";
#endif

    [DllImport(LIBNAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "CreateDataBase")]
    internal static extern IntPtr CreateDataBase( [MarshalAs(UnmanagedType.LPStr)]string path );

    [DllImport(LIBNAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "QueryTable")]
    internal static extern IntPtr QueryTable( IntPtr database, [MarshalAs(UnmanagedType.LPStr)]string sql );

    [DllImport(LIBNAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "RetTableRow")]
    internal static extern int RetTableRow( IntPtr table );

    [DllImport(LIBNAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "RetTableCol")]
    internal static extern int RetTableCol( IntPtr table );

    [DllImport(LIBNAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "RetTableStep")]
    internal static extern int RetTableStep( IntPtr table );

    [DllImport(LIBNAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "FreeTable")]
    internal static extern void FreeTable( IntPtr table );

    [DllImport(LIBNAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "TableStep")]
    internal static extern bool TableStep( IntPtr table );

    [DllImport(LIBNAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "TableGetString")]
    internal static extern IntPtr TableGetString( IntPtr table, [MarshalAs(UnmanagedType.LPStr)]string key );

    [DllImport(LIBNAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "TableGetDouble")]
    internal static extern double TableGetDouble( IntPtr table, [MarshalAs(UnmanagedType.LPStr)]string key, double def );

    [DllImport(LIBNAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "CloseDataBase")]
    internal static extern void CloseDataBase( IntPtr database );

    static int mReference = 0;
    static IntPtr m_db = (IntPtr)0;

    public static bool IsNullPtr()
    {
        return m_db == IntPtr.Zero;
    }

    public static bool IsNullPtr(IntPtr ptr)
    {
        return ptr == IntPtr.Zero;
    }

    public static bool OpenDatabase( string path )
    {
        //Util.Instance.Log("======创建表======="+path);
        mReference++;
        if (!IsNullPtr())
        {
            return true;
        }
        m_db = CreateDataBase(path);
        return !IsNullPtr();
    }

    static public SQLiteQuery Query( string sql )
    {
        return new SQLiteQuery(QueryInternal(sql));
    }

    internal static IntPtr QueryInternal( string sql )
    {
        if (string.IsNullOrEmpty(sql) || IsNullPtr())
        {
            return IntPtr.Zero;
        }
        return QueryTable(m_db, sql);
    }

    static public void Close()
    {
        mReference--;
        if (mReference <= 0 && !IsNullPtr())
        {
            CloseDataBase(m_db);
            m_db = IntPtr.Zero;
        }
    }
}

public class SQLiteQuery
{
    IntPtr m_table;
    internal SQLiteQuery( IntPtr table )
    {
        m_table = table;
    }

    public bool Step()
    {
        if (SqliteDataBase.IsNullPtr(m_table))
        {
            return false;
        }
        return SqliteDataBase.TableStep(m_table);
    }

    public int GetRowCount()
    {
        if (SqliteDataBase.IsNullPtr(m_table))
        {
            return 0;
        }

        return SqliteDataBase.RetTableRow(m_table);
    }

    public int GetColCount()
    {
        if (SqliteDataBase.IsNullPtr(m_table))
        {
            return 0;
        }

        return SqliteDataBase.RetTableCol(m_table);
    }

    public double GetDouble( string field )
    {
        return SqliteDataBase.TableGetDouble(m_table, field, 0);
    }

    public int GetInt( string field )
    {
        if (SqliteDataBase.IsNullPtr(m_table))
        {
            return 0;
        }

        return (int)SqliteDataBase.TableGetDouble(m_table, field, 0);
    }

    public uint GetUInt(string field)
    {
        if (SqliteDataBase.IsNullPtr(m_table))
        {
            return 0;
        }
        return (uint)SqliteDataBase.TableGetDouble(m_table, field, 0); 
    }

    public float GetFloat( string field )
    {
        if (SqliteDataBase.IsNullPtr(m_table))
        {
            return 0;
        }

        return (float)SqliteDataBase.TableGetDouble(m_table, field, 0);
    }

    public string GetString( string field )
    {
        if (SqliteDataBase.IsNullPtr(m_table))
        {
            return String.Empty;
        }

        IntPtr strPtr = SqliteDataBase.TableGetString(m_table, field);
        if (SqliteDataBase.IsNullPtr(strPtr))
        {
            return String.Empty;
        }
        return Marshal.PtrToStringAnsi(strPtr);
    }

    public bool GetBool(string field)
    {
        return GetInt(field) == 1 ? true : false;
    }

    public void Release()
    {
        if (SqliteDataBase.IsNullPtr(m_table))
        {
            return;
        }
        SqliteDataBase.FreeTable(m_table);
        m_table = IntPtr.Zero;
    }
}