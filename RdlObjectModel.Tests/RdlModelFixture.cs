using System.IO;
using NUnit.Framework;
using Microsoft.ReportingServices.RdlObjectModel;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;

namespace RdlObjectModel.Tests
{
	[TestFixture]
	public class RdlModelFixture
	{
		private Report _report;
		private const string DefaultDatasetName = "DefaultDataset";
		private const string TablixName = "Tablix1";

		[OneTimeSetUp]
		public void FixtureSetup()
		{
			_report = new Report();
			_report.DataSets.Add(new DataSet()
			{
				Name = DefaultDatasetName
			});
			_report.Body = new Body();
			
			_report.Body.Initialize();
			var tablix = new Tablix()
			{
				Name = TablixName
			};
			tablix.Initialize();
			var row = new TablixRow();
			row.Initialize();
			row.TablixCells.Add(new TablixCell());
			var col = new TablixColumn();
			col.Initialize();
			tablix.TablixBody.TablixColumns.Add(col);
			tablix.TablixBody.TablixRows.Add(row);
			tablix.TablixColumnHierarchy.TablixMembers.Add(new TablixMember());
			tablix.TablixRowHierarchy.TablixMembers.Add(new TablixMember());
			_report.Body.ReportItems.Add(tablix);
		}

		[Test]
	    public void RdlModelCreationTest()
		{
		    Assert.IsNotNull(_report);
	    }

		[Test]
		public void RdlModelSerializeTest()
		{
			var rdlXml = Serialize(_report);
			Assert.IsFalse(string.IsNullOrEmpty(rdlXml));
			Assert.IsTrue(rdlXml.Contains($"<DataSet Name=\"{DefaultDatasetName}\""));
			Assert.IsTrue(rdlXml.Contains("<Body>"));
			Assert.IsTrue(rdlXml.Contains($"<Tablix Name=\"{TablixName}\""));
		}

		[Test]
		public void RdlModelDeserializeTest()
		{
			var rdlXml = Serialize(_report);
			var report = Deserialize(rdlXml);
			Assert.IsNotNull(report);
			Assert.IsTrue(report.DataSets[0].Name == DefaultDatasetName);
		}

		private string Serialize(Report report)
		{
			using (var stream = new MemoryStream())
			{
				new RdlSerializer().Serialize(stream, report);
				return System.Text.Encoding.UTF8.GetString(stream.ToArray());
			}
		}

		private Report Deserialize(string reportRdl)
		{
			using (var stream = new MemoryStream())
			{
				StreamWriter writer = new StreamWriter(stream);
				writer.Write(reportRdl);
				writer.Flush();
				stream.Position = 0;
				return new RdlSerializer().Deserialize(stream);
			}
		}

	}
}
