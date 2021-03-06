﻿/*
 * Created by SharpDevelop.
 * User: Alexander Petrovskiy
 * Date: 11/18/2012
 * Time: 6:35 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

namespace TlAddinUnitTests.TestPlans
{
    using MbUnit.Framework;
    //using Autofac;
    //using Autofac.Builder;
    using Tmx;
    using Meyn.TestLink;

    /// <summary>
    /// Description of GetTestPlansTestFixture.
    /// </summary>
    [TestFixture]
    //[Parallelizable]
    public class GetTestPlansTestFixture
    {
        public GetTestPlansTestFixture()
        {
        }
        
        [SetUp]
        public void SetUp()
        {
            UnitTestingHelper.PrepareUnitTestDataStore();
        }
        
        [TearDown]
        public void TearDown()
        {
        }
        
        private System.Collections.Generic.List<TestPlan> getTestPlansFromProjectsInPipeline(
            System.Collections.Generic.List<TestProject> testProjects,
            System.Collections.Generic.List<TestPlan> testPlans,
            bool makeFail,
            bool inputNotSpecified)
        {
            TLGetTestPlanCmdletBase cmdlet = new TLGetTestPlanCmdletBase();

            if (inputNotSpecified) {
                cmdlet.InputObject = null;
            } else {
                TestProject[] projectsArray =
                    testProjects.ToArray();
                cmdlet.InputObject = projectsArray;
            }
            
            TLAddinData.CurrentTestLinkConnection =
                FakeTestLinkFactory.GetTestLinkWithTestPlans(testProjects, testPlans, null, null);
            
            if (inputNotSpecified) {
                TLAddinData.CurrentTestProject = testProjects[0];
            }
            
            if (makeFail) {
                TLAddinData.CurrentTestLinkConnection = null;
            }
            
            TLSrvGetTestPlanCommand command =
                new TLSrvGetTestPlanCommand(cmdlet);
            command.Execute();
            
            System.Collections.Generic.List<TestPlan> resultList =
                new System.Collections.Generic.List<TestPlan>();

            foreach (object tpl in PSTestLib.UnitTestOutput.LastOutput) {

                resultList.Add((TestPlan)tpl);
            }

            return resultList;
            
        }
        
        // project in the pipeline with broken connection
        [Test] //, Parallelizable]
        [Category("Fast")]
        public void TestProject_from_pipeline_with_broken_connection()
        {
            System.Collections.Generic.List<TestProject> listOfProjects =
                new System.Collections.Generic.List<TestProject>();
            listOfProjects.Add(
                FakeTestLinkFactory.GetTestProject(
                    "project",
                    "prj",
                    string.Empty));
            
            System.Collections.Generic.List<TestPlan> listOfTestPlans =
                new System.Collections.Generic.List<TestPlan>();
            listOfTestPlans.Add(
                FakeTestLinkFactory.GetTestPlan(
                    "testplan",
                    "notes",
                    true, // active
                    true, // is_public
                    true, // open
                    listOfProjects[0].id));
            
            System.Collections.Generic.List<TestPlan> resultList =
                getTestPlansFromProjectsInPipeline(listOfProjects, listOfTestPlans, true, false);

            Assert.AreEqual<System.Collections.Generic.List<TestPlan>>(
                (new System.Collections.Generic.List<TestPlan>()),
                resultList);
        }
        
        // project in the pipeline
        [Test] //, Parallelizable]
        [Category("Fast")]
        public void TestProject_from_pipeline()
        {
            System.Collections.Generic.List<TestProject> listOfProjects =
                new System.Collections.Generic.List<TestProject>();
            listOfProjects.Add(
                FakeTestLinkFactory.GetTestProject(
                    "project",
                    "prj",
                    string.Empty));
            
            System.Collections.Generic.List<TestPlan> listOfTestPlans =
                new System.Collections.Generic.List<TestPlan>();
            listOfTestPlans.Add(
                FakeTestLinkFactory.GetTestPlan(
                    "testplan",
                    "notes",
                    true, // active
                    true, // is_public
                    true, // open
                    listOfProjects[0].id));
            
            System.Collections.Generic.List<TestPlan> resultList =
                getTestPlansFromProjectsInPipeline(listOfProjects, listOfTestPlans, false, false);

            Assert.AreEqual<System.Collections.Generic.List<TestPlan>>(
                listOfTestPlans,
                resultList);
        }
        
        
        // projects in the pipeline
        [Test] //, Parallelizable]
        [Category("Fast")]
        public void TestProjects_from_pipeline()
        {
            System.Collections.Generic.List<TestProject> listOfProjects =
                new System.Collections.Generic.List<TestProject>();
            listOfProjects.Add(
                FakeTestLinkFactory.GetTestProject(
                    "project1",
                    "prj1",
                    string.Empty));
            listOfProjects.Add(
                FakeTestLinkFactory.GetTestProject(
                    "project2",
                    "prj2",
                    string.Empty));
            listOfProjects.Add(
                FakeTestLinkFactory.GetTestProject(
                    "project3",
                    "prj3",
                    string.Empty));
            
            System.Collections.Generic.List<TestPlan> listOfTestPlans =
                new System.Collections.Generic.List<TestPlan>();
            listOfTestPlans.Add(
                FakeTestLinkFactory.GetTestPlan(
                    "testplan1",
                    "notes",
                    true, // active
                    true, // is_public
                    true, // open
                    listOfProjects[0].id));
            listOfTestPlans.Add(
                FakeTestLinkFactory.GetTestPlan(
                    "testplan4",
                    "notes",
                    true, // active
                    true, // is_public
                    true, // open
                    111)); //listOfProjects[2].id));
            listOfTestPlans.Add(
                FakeTestLinkFactory.GetTestPlan(
                    "testplan2",
                    "notes",
                    true, // active
                    true, // is_public
                    true, // open
                    listOfProjects[1].id));
            listOfTestPlans.Add(
                FakeTestLinkFactory.GetTestPlan(
                    "testplan3",
                    "notes",
                    true, // active
                    true, // is_public
                    true, // open
                    listOfProjects[2].id));
            listOfTestPlans.Add(
                FakeTestLinkFactory.GetTestPlan(
                    "testplan5",
                    "notes",
                    true, // active
                    true, // is_public
                    true, // open
                    927));
            
            System.Collections.Generic.List<TestPlan> resultList =
                getTestPlansFromProjectsInPipeline(listOfProjects, listOfTestPlans, false, false);
            
            listOfTestPlans.RemoveAt(4);
            listOfTestPlans.RemoveAt(1);

            Assert.AreEqual<System.Collections.Generic.List<TestPlan>>(
            //Assert.AreEqual<Meyn.TestLink.TestPlan>(
                listOfTestPlans,
                resultList);
        }
        
        // project from the store - the same as from the pipeline
        
        // no project specified
        [Test] //, Parallelizable]
        [Category("Fast")]
        public void TestProject_not_specified()
        {
            System.Collections.Generic.List<TestProject> listOfProjects =
                new System.Collections.Generic.List<TestProject>();
            listOfProjects.Add(
                FakeTestLinkFactory.GetTestProject(
                    "project01",
                    "prj",
                    string.Empty));
            listOfProjects.Add(
                FakeTestLinkFactory.GetTestProject(
                    "project02",
                    "prj",
                    string.Empty));
            
            System.Collections.Generic.List<TestPlan> listOfTestPlans =
                new System.Collections.Generic.List<TestPlan>();
            listOfTestPlans.Add(
                FakeTestLinkFactory.GetTestPlan(
                    "testplan 01",
                    "notes",
                    true, // active
                    true, // is_public
                    true, // open
                    listOfProjects[0].id));
            listOfTestPlans.Add(
                FakeTestLinkFactory.GetTestPlan(
                    "testplan 02",
                    "notes",
                    true, // active
                    true, // is_public
                    true, // open
                    listOfProjects[1].id));
            listOfTestPlans.Add(
                FakeTestLinkFactory.GetTestPlan(
                    "testplan 03",
                    "notes",
                    true, // active
                    true, // is_public
                    true, // open
                    listOfProjects[0].id));
            
            System.Collections.Generic.List<TestPlan> resultList =
                getTestPlansFromProjectsInPipeline(listOfProjects, listOfTestPlans, false, true);
            
            listOfTestPlans.RemoveAt(1);

            Assert.AreEqual<System.Collections.Generic.List<TestPlan>>(
            //Assert.AreEqual<Meyn.TestLink.TestPlan>(
                listOfTestPlans,
                resultList);
        }
        
        // no project specified and there's no project in the store
        
        
        private System.Collections.Generic.List<TestPlan> getTestPlansByProjectName(
            TestProject currentTestProject,
            string[] testProjectNames,
            System.Collections.Generic.List<TestPlan> testPlans,
            bool makeFail,
            bool projectNamesNotSpecified)
        {

            TLGetTestPlanCmdletBase cmdlet = new TLGetTestPlanCmdletBase();

            if (projectNamesNotSpecified) {
                cmdlet.TestProjectName = null;
            } else {
                cmdlet.TestProjectName = testProjectNames;
            }
//cmdlet.WriteTrace(cmdlet, "getTestPlansByProjectName: 0001");
            System.Collections.Generic.List<TestProject> testProjects =
                new System.Collections.Generic.List<TestProject>();
            testProjects.Add(TLAddinData.CurrentTestProject);
//cmdlet.WriteTrace(cmdlet, "getTestPlansByProjectName: 0002");
            
            TLAddinData.CurrentTestLinkConnection =
                FakeTestLinkFactory.GetTestLinkWithTestPlans(testProjects, testPlans, null, null);
//cmdlet.WriteTrace(cmdlet, "getTestPlansByProjectName: 0003");
            if (projectNamesNotSpecified) {
//cmdlet.WriteTrace(cmdlet, "getTestPlansByProjectName: 0004_1");
                TLAddinData.CurrentTestProject = currentTestProject;
//cmdlet.WriteTrace(cmdlet, "getTestPlansByProjectName: 0004_2");
            }
            
            if (makeFail) {
//cmdlet.WriteTrace(cmdlet, "getTestPlansByProjectName: 0005_1");
                TLAddinData.CurrentTestLinkConnection = null;
//cmdlet.WriteTrace(cmdlet, "getTestPlansByProjectName: 0005_2");
            }
            
            TLSrvGetTestPlanCommand command =
                new TLSrvGetTestPlanCommand(cmdlet);
            command.Execute();
//cmdlet.WriteTrace(cmdlet, "getTestPlansByProjectName: 0006");
            System.Collections.Generic.List<TestPlan> resultList =
                new System.Collections.Generic.List<TestPlan>();
//cmdlet.WriteTrace(cmdlet, "getTestPlansByProjectName: 0007");
            foreach (object tpl in PSTestLib.UnitTestOutput.LastOutput) {
//cmdlet.WriteTrace(cmdlet, "getTestPlansByProjectName: 0008_1");
                resultList.Add((TestPlan)tpl);
//cmdlet.WriteTrace(cmdlet, "getTestPlansByProjectName: 0008_2");
            }
//cmdlet.WriteTrace(cmdlet, "getTestPlansByProjectName: 0009");
            return resultList;
            
        }
        
        
        // project in the pipeline with broken connection
        [Test] //, Parallelizable]
        [Category("Fast")]
        public void TestProject_by_name_with_broken_connection()
        {
            System.Collections.Generic.List<TestProject> listOfProjects =
                new System.Collections.Generic.List<TestProject>();
            listOfProjects.Add(
                FakeTestLinkFactory.GetTestProject(
                    "project",
                    "prj",
                    string.Empty));
            
            System.Collections.Generic.List<TestPlan> listOfTestPlans =
                new System.Collections.Generic.List<TestPlan>();
            listOfTestPlans.Add(
                FakeTestLinkFactory.GetTestPlan(
                    "testplan",
                    "notes",
                    true, // active
                    true, // is_public
                    true, // open
                    listOfProjects[0].id));
            
            System.Collections.Generic.List<TestPlan> resultList =
                getTestPlansByProjectName(
                    listOfProjects[0],
                    (new string[1]{ listOfProjects[0].name }),
                    listOfTestPlans, 
                    true, 
                    false);

            Assert.AreEqual<System.Collections.Generic.List<TestPlan>>(
            //Assert.AreEqual<Meyn.TestLink.TestPlan>(
                (new System.Collections.Generic.List<TestPlan>()),
                resultList);
        }
        
        // project in the pipeline
        [Test] //, Parallelizable]
        [Category("Fast")]
        public void TestProject_by_name()
        {
            System.Collections.Generic.List<TestProject> listOfProjects =
                new System.Collections.Generic.List<TestProject>();
            listOfProjects.Add(
                FakeTestLinkFactory.GetTestProject(
                    "project",
                    "prj",
                    string.Empty));
            
            System.Collections.Generic.List<TestPlan> listOfTestPlans =
                new System.Collections.Generic.List<TestPlan>();
            listOfTestPlans.Add(
                FakeTestLinkFactory.GetTestPlan(
                    "testplan",
                    "notes",
                    true, // active
                    true, // is_public
                    true, // open
                    listOfProjects[0].id));
//cmdlet.WriteTrace(cmdlet, "TestProject_by_name: 0001");
            System.Collections.Generic.List<TestPlan> resultList =
                getTestPlansByProjectName(
                    listOfProjects[0],
                    (new string[1]{ listOfProjects[0].name }),
                    listOfTestPlans, 
                    false, 
                    false);
//cmdlet.WriteTrace(cmdlet, "TestProject_by_name: 0002");
            Assert.AreEqual<System.Collections.Generic.List<TestPlan>>(
            //Assert.AreEqual<Meyn.TestLink.TestPlan>(
                (new System.Collections.Generic.List<TestPlan>()),
                resultList);
//cmdlet.WriteTrace(cmdlet, "TestProject_by_name: 0003");
        }
        
        
        // projects in the pipeline
        [Test] //, Parallelizable]
        [Category("Fast")]
        public void TestProjects_by_name()
        {
            System.Collections.Generic.List<TestProject> listOfProjects =
                new System.Collections.Generic.List<TestProject>();
            listOfProjects.Add(
                FakeTestLinkFactory.GetTestProject(
                    "project1",
                    "prj1",
                    string.Empty));
            listOfProjects.Add(
                FakeTestLinkFactory.GetTestProject(
                    "project2",
                    "prj2",
                    string.Empty));
            listOfProjects.Add(
                FakeTestLinkFactory.GetTestProject(
                    "project3",
                    "prj3",
                    string.Empty));
            
            System.Collections.Generic.List<TestPlan> listOfTestPlans =
                new System.Collections.Generic.List<TestPlan>();
            listOfTestPlans.Add(
                FakeTestLinkFactory.GetTestPlan(
                    "testplan1",
                    "notes",
                    true, // active
                    true, // is_public
                    true, // open
                    listOfProjects[0].id));
            listOfTestPlans.Add(
                FakeTestLinkFactory.GetTestPlan(
                    "testplan4",
                    "notes",
                    true, // active
                    true, // is_public
                    true, // open
                    111)); //listOfProjects[2].id));
            listOfTestPlans.Add(
                FakeTestLinkFactory.GetTestPlan(
                    "testplan2",
                    "notes",
                    true, // active
                    true, // is_public
                    true, // open
                    listOfProjects[1].id));
            listOfTestPlans.Add(
                FakeTestLinkFactory.GetTestPlan(
                    "testplan3",
                    "notes",
                    true, // active
                    true, // is_public
                    true, // open
                    listOfProjects[2].id));
            listOfTestPlans.Add(
                FakeTestLinkFactory.GetTestPlan(
                    "testplan5",
                    "notes",
                    true, // active
                    true, // is_public
                    true, // open
                    927));
            
            listOfTestPlans.RemoveAt(4);
            listOfTestPlans.RemoveAt(1);
//cmdlet.WriteTrace(cmdlet, "TestProjects_by_name: 0001");
            System.Collections.Generic.List<TestPlan> resultList =
                getTestPlansByProjectName(
                    listOfProjects[0],
                    (new string[3]{ listOfProjects[0].name, listOfProjects[1].name, listOfProjects[2].name }),
                    listOfTestPlans, 
                    false, 
                    false);
//cmdlet.WriteTrace(cmdlet, "TestProjects_by_name: 0002");
            Assert.AreEqual<System.Collections.Generic.List<TestPlan>>(
            //Assert.AreEqual<Meyn.TestLink.TestPlan>(
                (new System.Collections.Generic.List<TestPlan>()),
                resultList);
//cmdlet.WriteTrace(cmdlet, "TestProjects_by_name: 0003");
        }
        
        // project from the store - the same as from the pipeline
        
//        // no project specified
//        [Test] //, Parallelizable]
//        [Category("Fast")]
//        public void TestProject_not_specified()
//        {
//            System.Collections.Generic.List<TestProject> listOfProjects =
//                new System.Collections.Generic.List<TestProject>();
//            listOfProjects.Add(
//                FakeTestLinkFactory.GetTestProject(
//                    "project01",
//                    "prj",
//                    string.Empty));
//            listOfProjects.Add(
//                FakeTestLinkFactory.GetTestProject(
//                    "project02",
//                    "prj",
//                    string.Empty));
//            
//            System.Collections.Generic.List<TestPlan> listOfTestPlans =
//                new System.Collections.Generic.List<TestPlan>();
//            listOfTestPlans.Add(
//                FakeTestLinkFactory.GetTestPlan(
//                    "testplan 01",
//                    "notes",
//                    true, // active
//                    true, // is_public
//                    true, // open
//                    listOfProjects[0].id));
//            listOfTestPlans.Add(
//                FakeTestLinkFactory.GetTestPlan(
//                    "testplan 02",
//                    "notes",
//                    true, // active
//                    true, // is_public
//                    true, // open
//                    listOfProjects[1].id));
//            listOfTestPlans.Add(
//                FakeTestLinkFactory.GetTestPlan(
//                    "testplan 03",
//                    "notes",
//                    true, // active
//                    true, // is_public
//                    true, // open
//                    listOfProjects[0].id));
//            
//            System.Collections.Generic.List<TestPlan> resultList =
//                getTestPlansFromProjectsInPipeline(listOfProjects, listOfTestPlans, false, true);
//            
//            listOfTestPlans.RemoveAt(1);
//
//            Assert.AreEqual<System.Collections.Generic.List<TestPlan>>(
//                listOfTestPlans,
//                resultList);
//        }
        
        // no project specified and there's no project in the store
    }
}
