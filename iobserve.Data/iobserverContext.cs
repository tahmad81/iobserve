﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by the ContextGenerator.ttinclude code generation file.
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
using iobserve.Data;

namespace iobserve.Data	
{
	public partial class iobserverContext : OpenAccessContext, IiobserverContextUnitOfWork
	{
		private static string connectionStringName = @"Connection";
			
		private static BackendConfiguration backend = GetBackendConfiguration();
				
		private static MetadataSource metadataSource = XmlMetadataSource.FromAssemblyResource("iobserverContext.rlinq");
		
		public iobserverContext()
			:base(connectionStringName, backend, metadataSource)
		{ }
		
		public iobserverContext(string connection)
			:base(connection, backend, metadataSource)
		{ }
		
		public iobserverContext(BackendConfiguration backendConfiguration)
			:base(connectionStringName, backendConfiguration, metadataSource)
		{ }
			
		public iobserverContext(string connection, MetadataSource metadataSource)
			:base(connection, backend, metadataSource)
		{ }
		
		public iobserverContext(string connection, BackendConfiguration backendConfiguration, MetadataSource metadataSource)
			:base(connection, backendConfiguration, metadataSource)
		{ }
			
		public IQueryable<Report> Reports 
		{
			get
			{
				return this.GetAll<Report>();
			}
		}
		
		public IQueryable<V_report> V_reports 
		{
			get
			{
				return this.GetAll<V_report>();
			}
		}
		
		public IQueryable<V_question> V_questions 
		{
			get
			{
				return this.GetAll<V_question>();
			}
		}
		
		public static BackendConfiguration GetBackendConfiguration()
		{
			BackendConfiguration backend = new BackendConfiguration();
			backend.Backend = "Azure";
			backend.ProviderName = "System.Data.SqlClient";
		
			CustomizeBackendConfiguration(ref backend);
		
			return backend;
		}
		
		/// <summary>
		/// Allows you to customize the BackendConfiguration of iobserverContext.
		/// </summary>
		/// <param name="config">The BackendConfiguration of iobserverContext.</param>
		static partial void CustomizeBackendConfiguration(ref BackendConfiguration config);
		
	}
	
	public interface IiobserverContextUnitOfWork : IUnitOfWork
	{
		IQueryable<Report> Reports
		{
			get;
		}
		IQueryable<V_report> V_reports
		{
			get;
		}
		IQueryable<V_question> V_questions
		{
			get;
		}
	}
}
#pragma warning restore 1591
