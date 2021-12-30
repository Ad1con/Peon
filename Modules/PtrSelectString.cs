﻿using System;
using System.Collections.Generic;
using System.Linq;
using FFXIVClientStructs.FFXIV.Client.UI;
using FFXIVClientStructs.FFXIV.Component.GUI;
using Peon.Bothers;

namespace Peon.Modules
{
    // Also usable by SystemMenu and SelectIconString
    public unsafe struct PtrSelectString
    {
        public AddonSelectString* Pointer;

        public static implicit operator PtrSelectString(IntPtr ptr)
            => new() { Pointer = Module.Cast<AddonSelectString>(ptr) };

        public static implicit operator bool(PtrSelectString ptr)
            => ptr.Pointer != null;

        public bool Select(int idx)
            => Module.ClickList(&Pointer->PopupMenu.PopupMenu, Pointer->PopupMenu.PopupMenu.List->AtkComponentBase.OwnerNode, idx);

        public string Description()
            => Module.TextNodeToString((AtkTextNode*) Pointer->AtkUnitBase.UldManager.NodeList[3]);

        public int Count
            => Pointer->PopupMenu.PopupMenu.List->ListLength;

        public string ItemText(int idx)
            => Module.TextNodeToString(Pointer->PopupMenu.PopupMenu.List->ItemRendererList[idx]
               .AtkComponentListItemRenderer->AtkComponentButton.ButtonTextNode);

        public IEnumerable<string> EnumerateTexts()
            => Enumerable.Range(0, Count).Select(ItemText);

        public string[] ItemTexts()
            => EnumerateTexts().ToArray();

        public bool Select(CompareString text)
            => Module.ClickList(&Pointer->PopupMenu.PopupMenu, Pointer->PopupMenu.PopupMenu.List->AtkComponentBase.OwnerNode,
                item => text.Matches(Module.TextNodeToString(item->AtkComponentButton.ButtonTextNode)));
    }
}
