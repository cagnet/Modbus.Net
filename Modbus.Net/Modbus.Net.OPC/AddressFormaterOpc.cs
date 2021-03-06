﻿using System;
using System.Linq;

namespace Modbus.Net.OPC
{
    /// <summary>
    ///     Opc地址编码器
    /// </summary>
    public class AddressFormaterOpc : AddressFormater
    {
        /// <summary>
        ///     协议构造器
        /// </summary>
        /// <param name="tagGeter">如何通过BaseMachine和AddressUnit构造Opc的标签</param>
        /// <param name="machine">调用这个编码器的设备</param>
        /// <param name="seperator">每两个标签之间用什么符号隔开，默认为/</param>
        public AddressFormaterOpc(Func<BaseMachine, AddressUnit, string[]> tagGeter, BaseMachine machine,
            char seperator = '/')
        {
            Machine = machine;
            TagGeter = tagGeter;
            Seperator = seperator;
        }

        public BaseMachine Machine { get; set; }

        protected Func<BaseMachine, AddressUnit, string[]> TagGeter { get; set; }

        protected char Seperator { get; set; }

        public override string FormatAddress(string area, int address)
        {
            var findAddress = Machine?.GetAddresses.FirstOrDefault(p => p.Area == area && p.Address == address);
            if (findAddress == null) return null;
            var strings = TagGeter(Machine, findAddress);
            var ans = "";
            for (var i = 0; i < strings.Length; i++)
            {
                ans += strings[i].Trim().Replace(" ", "") + Seperator;
            }
            ans = ans.Substring(0, ans.Length - 1);
            return ans;
        }

        public override string FormatAddress(string area, int address, int subAddress)
        {
            return FormatAddress(area, address);
        }
    }
}