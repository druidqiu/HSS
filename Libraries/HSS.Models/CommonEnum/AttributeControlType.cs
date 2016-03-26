﻿
namespace HSS.Core.Domain.Common
{
    /// <summary>
    /// 控件枚举
    /// </summary>
    public enum AttributeControlType:int 
    {
        /// <summary>
        /// Dropdown list
        /// </summary>
        DropdownList = 1,
        /// <summary>
        /// Radio list
        /// </summary>
        RadioList = 2,
        /// <summary>
        /// Checkboxes
        /// </summary>
        Checkboxes = 3,
        /// <summary>
        /// TextBox
        /// </summary>
        TextBox = 4,
        /// <summary>
        /// Multiline textbox
        /// </summary>
        MultilineTextbox = 10,
        /// <summary>
        /// Datepicker
        /// </summary>
        Datepicker = 20,
        /// <summary>
        /// File upload control
        /// </summary>
        FileUpload = 30,
        /// <summary>
        /// Color squares
        /// </summary>
        ColorSquares = 40,
        /// <summary>
        /// Read-only checkboxes
        /// </summary>
        ReadonlyCheckboxes = 50,
    }
}
