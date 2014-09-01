/**
 * Autogenerated by Thrift Compiler (0.9.1)
 *
 * DO NOT EDIT UNLESS YOU ARE SURE THAT YOU KNOW WHAT YOU ARE DOING
 *  @generated
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Thrift;
using Thrift.Collections;
using System.Runtime.Serialization;
using Thrift.Protocol;
using Thrift.Transport;

namespace Wordbook
{

  #if !SILVERLIGHT
  [Serializable]
  #endif
  public partial class Wordbook : TBase
  {
    private int _count;
    private List<Word> _words;

    public int Count
    {
      get
      {
        return _count;
      }
      set
      {
        __isset.count = true;
        this._count = value;
      }
    }

    public List<Word> Words
    {
      get
      {
        return _words;
      }
      set
      {
        __isset.words = true;
        this._words = value;
      }
    }


    public Isset __isset;
    #if !SILVERLIGHT
    [Serializable]
    #endif
    public struct Isset {
      public bool count;
      public bool words;
    }

    public Wordbook() {
    }

    public void Read (TProtocol iprot)
    {
      TField field;
      iprot.ReadStructBegin();
      while (true)
      {
        field = iprot.ReadFieldBegin();
        if (field.Type == TType.Stop) { 
          break;
        }
        switch (field.ID)
        {
          case 1:
            if (field.Type == TType.I32) {
              Count = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 2:
            if (field.Type == TType.List) {
              {
                Words = new List<Word>();
                TList _list0 = iprot.ReadListBegin();
                for( int _i1 = 0; _i1 < _list0.Count; ++_i1)
                {
                  Word _elem2 = new Word();
                  _elem2 = new Word();
                  _elem2.Read(iprot);
                  Words.Add(_elem2);
                }
                iprot.ReadListEnd();
              }
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          default: 
            TProtocolUtil.Skip(iprot, field.Type);
            break;
        }
        iprot.ReadFieldEnd();
      }
      iprot.ReadStructEnd();
    }

    public void Write(TProtocol oprot) {
      TStruct struc = new TStruct("Wordbook");
      oprot.WriteStructBegin(struc);
      TField field = new TField();
      if (__isset.count) {
        field.Name = "count";
        field.Type = TType.I32;
        field.ID = 1;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(Count);
        oprot.WriteFieldEnd();
      }
      if (Words != null && __isset.words) {
        field.Name = "words";
        field.Type = TType.List;
        field.ID = 2;
        oprot.WriteFieldBegin(field);
        {
          oprot.WriteListBegin(new TList(TType.Struct, Words.Count));
          foreach (Word _iter3 in Words)
          {
            _iter3.Write(oprot);
          }
          oprot.WriteListEnd();
        }
        oprot.WriteFieldEnd();
      }
      oprot.WriteFieldStop();
      oprot.WriteStructEnd();
    }

    public override string ToString() {
      StringBuilder sb = new StringBuilder("Wordbook(");
      sb.Append("Count: ");
      sb.Append(Count);
      sb.Append(",Words: ");
      sb.Append(Words);
      sb.Append(")");
      return sb.ToString();
    }

  }

}