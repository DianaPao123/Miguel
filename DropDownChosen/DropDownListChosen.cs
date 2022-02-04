using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Web.UI;

[assembly: TagPrefix("CustomDropDown", "ucc")]
[assembly: WebResource("DropDownChosen.css.chosen.css", "text/css", PerformSubstitution = true)]
[assembly: WebResource("DropDownChosen.css.chosen.min.css", "text/css")]
[assembly: WebResource("DropDownChosen.css.chosen-sprite@2x.png", "image/png")]
[assembly: WebResource("DropDownChosen.css.chosen-sprite.png", "image/png")]
namespace CustomDropDown
{
    [ToolboxData(@"<{0}:DropDownListChosen runat=""server"" ></{0}:DropDownListChosen>"),
    ToolboxBitmapAttribute(typeof(DropDownListChosen), "DropDownListChosen.bmp")]
    public class DropDownListChosen : System.Web.UI.WebControls.DropDownList
    {
        // ####################
        #region Local Variables
        string sCssClass = "";
        #endregion


        // ###################
        #region Public Properties

        public override string ID
        {
            set
            {
                base.ID = value;
                InitializeProperties();
            }
        }


        //Chosen automatically sets the default field text ("Choose a country...") by reading the select element's data-placeholder value. If no data-placeholder value is present, it will default to "Select an Option" or "Select Some Options" depending on whether the select is single or multiple. You can change these elements in the plugin js file as you see fit.
        [Description("Set the text as watermark")]
        [Category("Behavior")]
        [Browsable(true)]
        [Themeable(true)]
        [DefaultValue("")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string DataPlaceHolder
        {
            get
            {
                object o = ViewState["DataPlaceholder"];
                if (o == null)
                    return string.Empty; ;
                return (string)o;
            }
            set { ViewState["DataPlaceholder"] = value; }
        }


        [Description("Set the Message when no results")]
        [Category("Behavior")]
        [Browsable(true)]
        [Themeable(true)]
        [DefaultValue("")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string NoResultsText
        {
            get
            {
                object o = ViewState["NoResultsText"];
                if (o == null)
                    return string.Empty; ;
                return (string)o;
            }
            set { ViewState["NoResultsText"] = value; }
        }


        [Description("When a single select box isnt a required field, you can set this option to true and Chosen will add a UI element for option deselection. This will only work if the first option has blank text.")]
        [Category("Behavior")]
        [Browsable(true)]
        [Themeable(true)]
        [DefaultValue(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool AllowSingleDeselect
        {
            get
            {
                object o = ViewState["AllowSingleDeselect"];
                if (o == null)
                    return false;
                return (bool)o;
            }
            set { ViewState["AllowSingleDeselect"] = value; }
        }


        [Description("The option can be specified to hide the search input on single selects if there are fewer than (n) options.")]
        [Category("Behavior")]
        [Browsable(true)]
        [Themeable(true)]
        [DefaultValue(0)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int DisableSearchThreshold
        {
            get
            {
                object o = ViewState["DisableSearchThreshold"];
                if (o == null)
                    return 0;
                return (int)o;
            }
            set { ViewState["DisableSearchThreshold"] = value; }
        }

        #endregion Public Properties


        // ###################
        #region Control Init

        /// <summary>
        /// Initialize this control.  We need to call RegisterRequiresPostBack
        /// </summary>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            string css = "<link href=\"" + Page.ClientScript.GetWebResourceUrl(this.GetType(), "DropDownChosen.css.chosen.css") + "\" type=\"text/css\" rel=\"stylesheet\" />";

            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "cssFile", css, false);
            Page.RegisterRequiresPostBack(this);
        }

        #endregion Control Init

        // ###################
        #region Render

        /// <summary>
        /// Override the Render Event
        /// </summary>
        /// <param name="writer"></param>
        protected override void Render(HtmlTextWriter writer)
        {
            //First element empty if AllowSingleDeselect true or DataPlaceHolder is not empty or null.
            if (AllowSingleDeselect || !string.IsNullOrEmpty(DataPlaceHolder))
            {
                this.Items.Insert(0, new ListItem(string.Empty, "0"));
            }           

            string script = string.Format(@" 
                                          $('.chzn-select').chosen({{
                                            allow_single_deselect: {0},
                                            disable_search_threshold:{1},
                                            no_results_text:'{2}'}}
                                          );", AllowSingleDeselect.ToString().ToLower(), DisableSearchThreshold, NoResultsText);

            this.AddCssClass("chzn-select");

            if (!String.IsNullOrEmpty(this.sCssClass)) writer.AddAttribute(HtmlTextWriterAttribute.Class, this.sCssClass);
            if (!String.IsNullOrEmpty(this.DataPlaceHolder)) writer.AddAttribute("data-placeholder", this.DataPlaceHolder);

            if (ScriptManager.GetCurrent(this.Page) != null)
                ScriptManager.RegisterStartupScript(this, this.GetType(), "init" + ClientID, script, true);
            else
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "init" + ClientID, script, true);

            base.Render(writer);
        }

        #endregion Render



        // ###################
        #region OnPreRender

        /// <summary>
        /// The OnPreRender event.  Use this event to raise any events that need to be fired before our controls are rendered.
        /// </summary>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            JavaScriptHelper.Include_JQueryMin(Page.ClientScript);
            JavaScriptHelper.Include_ChosenJQuery(Page.ClientScript);
        }

        #endregion Control Events

        // ###################

        #region Private Methods
        /// <summary>
        /// Adds the CSS class.
        /// </summary>
        /// <param name="cssClass">The CSS class.</param>
        private void AddCssClass(string cssClass)
        {
            if (String.IsNullOrEmpty(this.sCssClass))
            {
                this.sCssClass = cssClass;
            }
            else
            {
                this.sCssClass += " " + cssClass;
            }
        }

        /// <summary>
        /// Properties inicialization with default values.
        /// </summary>
        private void InitializeProperties()
        {
            DisableSearchThreshold = 10;
        }
        #endregion

    }
}
