using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;


[assembly: WebResource("DropDownChosen.js.chosen.jquery.js", "application/x-javascript")]
[assembly: WebResource("DropDownChosen.js.chosen.jquery.min.js", "application/x-javascript")]
[assembly: WebResource("DropDownChosen.js.chosen.proto.js", "application/x-javascript")]
[assembly: WebResource("DropDownChosen.js.chosen.proto.min.js", "application/x-javascript")]
[assembly: WebResource("DropDownChosen.js.jquery-1.6.4.min.js", "application/x-javascript")]

namespace CustomDropDown
{
    class JavaScriptHelper
    {
        #region Constants

        private const string NAME_CHOSEN_JQUERY = "DropDownChosen.js.chosen.jquery.js";
        private const string NAME_CHOSEN_JQUERY_MIN = "DropDownChosen.js.chosen.jquery.min.js";
        private const string NAME_CHOSEN_PROTO = "DropDownChosen.js.chosen.proto.js";
        private const string NAME_CHOSEN_PROTO_MIN = "DropDownChosen.js.chosen.proto.min.js";
        private const string NAME_JQUERY_MIN = "DropDownChosen.js.jquery-1.6.4.min.js";

        #endregion

        #region Public Methods

        /// <summary>
        /// Includes DropDownChosen.js.chosen.jquery.js in the page.
        /// </summary>
        /// <param name="manager">Accessible via Page.ClientScript.</param>
        public static void Include_ChosenJQuery(ClientScriptManager manager)
        {
            // chosen.jquery.js
            IncludeJavaScript(manager, NAME_CHOSEN_JQUERY);
        }

        /// <summary>
        /// Includes DropDownChosen.js.chosen.jquery.min.js in the page.
        /// </summary>
        /// <param name="manager">Accessible via Page.ClientScript.</param>
        public static void Include_ChosenJQueryMin(ClientScriptManager manager)
        {
            // chosen.jquery.min.js.
            IncludeJavaScript(manager, NAME_CHOSEN_JQUERY_MIN);
        }

        /// <summary>
        /// Includes DropDownChosen.js.chosen.proto.js in the page.
        /// </summary>
        /// <param name="manager">Accessible via Page.ClientScript.</param>
        public static void Include_ChosenProto(ClientScriptManager manager)
        {
            // chosen.proto.js.
            IncludeJavaScript(manager, NAME_CHOSEN_PROTO);
        }

        /// <summary>
        /// Includes DropDownChosen.js.chosen.proto.min.js in the page.
        /// </summary>
        /// <param name="manager">Accessible via Page.ClientScript.</param>
        public static void Include_ChosenProtoMin(ClientScriptManager manager)
        {
            // chosen.proto.min.js.
            IncludeJavaScript(manager, NAME_CHOSEN_PROTO_MIN);
        }      

        /// <summary>
        /// Includes jquery-1.6.4.min.js in the page.
        /// </summary>
        /// <param name="manager">Accessible via Page.ClientScript.</param>
        public static void Include_JQueryMin(ClientScriptManager manager)
        {
            // Include jquery-1.6.4.min.js.
            IncludeJavaScript(manager, NAME_JQUERY_MIN);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Includes the specified embedded JavaScript file in the page.
        /// </summary>
        /// <param name="manager">Accessible via Page.ClientScript.</param>
        /// <param name="resourceName">The name used to identify the 
        /// embedded JavaScript file.
        /// </param>
        private static void IncludeJavaScript
        (ClientScriptManager manager, string resourceName)
        {
            var type = typeof(JavaScriptHelper);
            manager.RegisterClientScriptResource(type, resourceName);
        }

        #endregion
    }
}
