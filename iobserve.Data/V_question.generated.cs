#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by the ClassGenerator.ttinclude code generation file.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Common;
using System.Collections.Generic;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Metadata;
using Telerik.OpenAccess.Data.Common;
using Telerik.OpenAccess.Metadata.Fluent;
using Telerik.OpenAccess.Metadata.Fluent.Advanced;

namespace iobserve.Data	
{
	public partial class V_question
	{
		private string _thumbnail;
		public virtual string Thumbnail
		{
			get
			{
				return this._thumbnail;
			}
			set
			{
				this._thumbnail = value;
			}
		}
		
		private int? _seqno;
		public virtual int? Seqno
		{
			get
			{
				return this._seqno;
			}
			set
			{
				this._seqno = value;
			}
		}
		
		private string _scenario_name;
		public virtual string Scenario_name
		{
			get
			{
				return this._scenario_name;
			}
			set
			{
				this._scenario_name = value;
			}
		}
		
		private string _scenario_id;
		public virtual string Scenario_id
		{
			get
			{
				return this._scenario_id;
			}
			set
			{
				this._scenario_id = value;
			}
		}
		
		private string _questionnaire_id;
		public virtual string Questionnaire_id
		{
			get
			{
				return this._questionnaire_id;
			}
			set
			{
				this._questionnaire_id = value;
			}
		}
		
		private string _question_text;
		public virtual string Question_text
		{
			get
			{
				return this._question_text;
			}
			set
			{
				this._question_text = value;
			}
		}
		
		private string _question_id;
		public virtual string Question_id
		{
			get
			{
				return this._question_id;
			}
			set
			{
				this._question_id = value;
			}
		}
		
		private string _qr_id;
		public virtual string Qr_id
		{
			get
			{
				return this._qr_id;
			}
			set
			{
				this._qr_id = value;
			}
		}
		
		private string _location_scenario_id;
		public virtual string Location_scenario_id
		{
			get
			{
				return this._location_scenario_id;
			}
			set
			{
				this._location_scenario_id = value;
			}
		}
		
		private string _location_id;
		public virtual string Location_id
		{
			get
			{
				return this._location_id;
			}
			set
			{
				this._location_id = value;
			}
		}
		
		private string _language_code;
		public virtual string Language_code
		{
			get
			{
				return this._language_code;
			}
			set
			{
				this._language_code = value;
			}
		}
		
		private string _answer_options_text;
		public virtual string Answer_options_text
		{
			get
			{
				return this._answer_options_text;
			}
			set
			{
				this._answer_options_text = value;
			}
		}
		
		private string _answer_format;
		public virtual string Answer_format
		{
			get
			{
				return this._answer_format;
			}
			set
			{
				this._answer_format = value;
			}
		}
		
	}
}
#pragma warning restore 1591
