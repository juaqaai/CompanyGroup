// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:1.5.0.0
//      Runtime Version:4.0.30319.17011
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
namespace Orchard.Specs
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.5.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("Blog")]
    public partial class BlogFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "Blogs.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Blog", "In order to add blogs to my site\r\nAs an author\r\nI want to create blogs and create" +
                    ", publish and edit blog posts", GenerationTargetLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.TestFixtureTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("In the admin (menu) there is a link to create a Blog")]
        public virtual void InTheAdminMenuThereIsALinkToCreateABlog()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("In the admin (menu) there is a link to create a Blog", ((string[])(null)));
#line 6
this.ScenarioSetup(scenarioInfo);
#line 7
    testRunner.Given("I have installed Orchard");
#line 8
    testRunner.When("I go to \"admin\"");
#line 9
    testRunner.Then("I should see \"<a[^>]*href=\"/Admin/Blogs/Create\"[^>]*>Blog</a>\"");
#line hidden
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("I can create a new blog and blog post")]
        public virtual void ICanCreateANewBlogAndBlogPost()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("I can create a new blog and blog post", ((string[])(null)));
#line 11
this.ScenarioSetup(scenarioInfo);
#line 12
    testRunner.Given("I have installed Orchard");
#line 13
    testRunner.When("I go to \"admin/blogs/create\"");
#line hidden
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "name",
                        "value"});
            table1.AddRow(new string[] {
                        "Routable.Title",
                        "My Blog"});
#line 14
        testRunner.And("I fill in", ((string)(null)), table1);
#line 17
        testRunner.And("I hit \"Save\"");
#line 18
        testRunner.And("I go to \"admin/blogs\"");
#line 19
        testRunner.And("I follow \"My Blog\"");
#line 20
        testRunner.And("I follow \"New Post\" where class name has \"primaryAction\"");
#line hidden
            TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                        "name",
                        "value"});
            table2.AddRow(new string[] {
                        "Routable.Title",
                        "My Post"});
            table2.AddRow(new string[] {
                        "Body.Text",
                        "Hi there."});
#line 21
        testRunner.And("I fill in", ((string)(null)), table2);
#line 25
        testRunner.And("I hit \"Publish Now\"");
#line 26
        testRunner.And("I am redirected");
#line 27
    testRunner.Then("I should see \"Your Blog Post has been created.\"");
#line 28
    testRunner.When("I go to \"my-blog\"");
#line 29
    testRunner.Then("I should see \"<h1[^>]*>.*?My Blog.*?</h1>\"");
#line 30
        testRunner.And("I should see \"<h1[^>]*>.*?My Post.*?</h1>\"");
#line 31
    testRunner.When("I go to \"my-blog/my-post\"");
#line 32
    testRunner.Then("I should see \"<h1[^>]*>.*?My Post.*?</h1>\"");
#line 33
        testRunner.And("I should see \"Hi there.\"");
#line hidden
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("I can create a new blog with multiple blog posts each with the same title and uni" +
            "que slugs are generated or given for said posts")]
        public virtual void ICanCreateANewBlogWithMultipleBlogPostsEachWithTheSameTitleAndUniqueSlugsAreGeneratedOrGivenForSaidPosts()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("I can create a new blog with multiple blog posts each with the same title and uni" +
                    "que slugs are generated or given for said posts", ((string[])(null)));
#line 35
this.ScenarioSetup(scenarioInfo);
#line 36
    testRunner.Given("I have installed Orchard");
#line 37
    testRunner.When("I go to \"admin/blogs/create\"");
#line hidden
            TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
                        "name",
                        "value"});
            table3.AddRow(new string[] {
                        "Routable.Title",
                        "My Blog"});
#line 38
        testRunner.And("I fill in", ((string)(null)), table3);
#line 41
        testRunner.And("I hit \"Save\"");
#line 42
        testRunner.And("I go to \"admin/blogs\"");
#line 43
        testRunner.And("I follow \"My Blog\"");
#line 44
        testRunner.And("I follow \"New Post\" where class name has \"primaryAction\"");
#line hidden
            TechTalk.SpecFlow.Table table4 = new TechTalk.SpecFlow.Table(new string[] {
                        "name",
                        "value"});
            table4.AddRow(new string[] {
                        "Routable.Title",
                        "My Post"});
            table4.AddRow(new string[] {
                        "Body.Text",
                        "Hi there."});
#line 45
        testRunner.And("I fill in", ((string)(null)), table4);
#line 49
        testRunner.And("I hit \"Publish Now\"");
#line 50
        testRunner.And("I go to \"my-blog/my-post\"");
#line 51
    testRunner.Then("I should see \"<h1[^>]*>.*?My Post.*?</h1>\"");
#line 52
        testRunner.And("I should see \"Hi there.\"");
#line 53
    testRunner.When("I go to \"admin/blogs\"");
#line 54
        testRunner.And("I follow \"My Blog\"");
#line 55
        testRunner.And("I follow \"New Post\" where class name has \"primaryAction\"");
#line hidden
            TechTalk.SpecFlow.Table table5 = new TechTalk.SpecFlow.Table(new string[] {
                        "name",
                        "value"});
            table5.AddRow(new string[] {
                        "Routable.Title",
                        "My Post"});
            table5.AddRow(new string[] {
                        "Body.Text",
                        "Hi there, again."});
#line 56
        testRunner.And("I fill in", ((string)(null)), table5);
#line 60
        testRunner.And("I hit \"Publish Now\"");
#line 61
        testRunner.And("I go to \"my-blog/my-post-2\"");
#line 62
    testRunner.Then("I should see \"<h1[^>]*>.*?My Post.*?</h1>\"");
#line 63
        testRunner.And("I should see \"Hi there, again.\"");
#line 64
    testRunner.When("I go to \"admin/blogs\"");
#line 65
        testRunner.And("I follow \"My Blog\"");
#line 66
        testRunner.And("I follow \"New Post\" where class name has \"primaryAction\"");
#line hidden
            TechTalk.SpecFlow.Table table6 = new TechTalk.SpecFlow.Table(new string[] {
                        "name",
                        "value"});
            table6.AddRow(new string[] {
                        "Routable.Title",
                        "My Post"});
            table6.AddRow(new string[] {
                        "Routable.Slug",
                        "my-post"});
            table6.AddRow(new string[] {
                        "Body.Text",
                        "Are you still there?"});
#line 67
        testRunner.And("I fill in", ((string)(null)), table6);
#line 72
        testRunner.And("I hit \"Publish Now\"");
#line 73
        testRunner.And("I go to \"my-blog/my-post-3\"");
#line 74
    testRunner.Then("I should see \"<h1[^>]*>.*?My Post.*?</h1>\"");
#line 75
        testRunner.And("I should see \"Are you still there?\"");
#line hidden
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("I can create a new blog and blog post and when I change the slug of the blog the " +
            "path of the plog post is updated")]
        public virtual void ICanCreateANewBlogAndBlogPostAndWhenIChangeTheSlugOfTheBlogThePathOfThePlogPostIsUpdated()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("I can create a new blog and blog post and when I change the slug of the blog the " +
                    "path of the plog post is updated", ((string[])(null)));
#line 77
this.ScenarioSetup(scenarioInfo);
#line 78
    testRunner.Given("I have installed Orchard");
#line 79
    testRunner.When("I go to \"admin/blogs/create\"");
#line hidden
            TechTalk.SpecFlow.Table table7 = new TechTalk.SpecFlow.Table(new string[] {
                        "name",
                        "value"});
            table7.AddRow(new string[] {
                        "Routable.Title",
                        "My Blog"});
#line 80
        testRunner.And("I fill in", ((string)(null)), table7);
#line 83
        testRunner.And("I hit \"Save\"");
#line 84
        testRunner.And("I go to \"my-blog\"");
#line 85
    testRunner.Then("I should see \"<h1[^>]*>.*?My Blog.*?</h1>\"");
#line 86
    testRunner.When("I go to \"admin/blogs\"");
#line 87
        testRunner.And("I follow \"My Blog\"");
#line 88
        testRunner.And("I follow \"New Post\" where class name has \"primaryAction\"");
#line hidden
            TechTalk.SpecFlow.Table table8 = new TechTalk.SpecFlow.Table(new string[] {
                        "name",
                        "value"});
            table8.AddRow(new string[] {
                        "Routable.Title",
                        "My Post"});
            table8.AddRow(new string[] {
                        "Body.Text",
                        "Hi there."});
#line 89
        testRunner.And("I fill in", ((string)(null)), table8);
#line 93
        testRunner.And("I hit \"Publish Now\"");
#line 94
        testRunner.And("I go to \"my-blog/my-post\"");
#line 95
    testRunner.Then("I should see \"<h1[^>]*>.*?My Post.*?</h1>\"");
#line 96
        testRunner.And("I should see \"Hi there.\"");
#line 97
    testRunner.When("I go to \"admin/blogs\"");
#line 98
        testRunner.And("I follow \"My Blog\"");
#line 99
        testRunner.And("I follow \"Blog Properties\"");
#line hidden
            TechTalk.SpecFlow.Table table9 = new TechTalk.SpecFlow.Table(new string[] {
                        "name",
                        "value"});
            table9.AddRow(new string[] {
                        "Routable.Slug",
                        "my-other-blog"});
#line 100
        testRunner.And("I fill in", ((string)(null)), table9);
#line 103
        testRunner.And("I hit \"Save\"");
#line 104
        testRunner.And("I go to \"my-other-blog\"");
#line 105
    testRunner.Then("I should see \"<h1[^>]*>.*?My Blog.*?</h1>\"");
#line 106
    testRunner.When("I go to \"my-other-blog/my-post\"");
#line 107
    testRunner.Then("I should see \"<h1[^>]*>.*?My Post.*?</h1>\"");
#line 108
        testRunner.And("I should see \"Hi there.\"");
#line hidden
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("When viewing a blog the user agent is given an RSS feed of the blog\'s posts")]
        public virtual void WhenViewingABlogTheUserAgentIsGivenAnRSSFeedOfTheBlogSPosts()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("When viewing a blog the user agent is given an RSS feed of the blog\'s posts", ((string[])(null)));
#line 110
this.ScenarioSetup(scenarioInfo);
#line 111
    testRunner.Given("I have installed Orchard");
#line 112
    testRunner.When("I go to \"admin/blogs/create\"");
#line hidden
            TechTalk.SpecFlow.Table table10 = new TechTalk.SpecFlow.Table(new string[] {
                        "name",
                        "value"});
            table10.AddRow(new string[] {
                        "Routable.Title",
                        "My Blog"});
#line 113
        testRunner.And("I fill in", ((string)(null)), table10);
#line 116
        testRunner.And("I hit \"Save\"");
#line 117
        testRunner.And("I go to \"admin/blogs\"");
#line 118
        testRunner.And("I follow \"My Blog\"");
#line 119
        testRunner.And("I follow \"New Post\" where class name has \"primaryAction\"");
#line hidden
            TechTalk.SpecFlow.Table table11 = new TechTalk.SpecFlow.Table(new string[] {
                        "name",
                        "value"});
            table11.AddRow(new string[] {
                        "Routable.Title",
                        "My Post"});
            table11.AddRow(new string[] {
                        "Body.Text",
                        "Hi there."});
#line 120
        testRunner.And("I fill in", ((string)(null)), table11);
#line 124
        testRunner.And("I hit \"Publish Now\"");
#line 125
        testRunner.And("I am redirected");
#line 126
        testRunner.And("I go to \"my-blog/my-post\"");
#line 127
    testRunner.Then("I should see \"<link rel=\"alternate\" type=\"application/rss\\+xml\" title=\"My Blog\" h" +
                    "ref=\"/rss\\?containerid=\\d+\" />\"");
#line hidden
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Enabling remote blog publishing inserts the appropriate metaweblogapi markup into" +
            " the blog\'s page")]
        public virtual void EnablingRemoteBlogPublishingInsertsTheAppropriateMetaweblogapiMarkupIntoTheBlogSPage()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Enabling remote blog publishing inserts the appropriate metaweblogapi markup into" +
                    " the blog\'s page", ((string[])(null)));
#line 130
this.ScenarioSetup(scenarioInfo);
#line 131
    testRunner.Given("I have installed Orchard");
#line 132
        testRunner.And("I have enabled \"XmlRpc\"");
#line 133
        testRunner.And("I have enabled \"Orchard.Blogs.RemotePublishing\"");
#line 134
    testRunner.When("I go to \"admin/blogs/create\"");
#line hidden
            TechTalk.SpecFlow.Table table12 = new TechTalk.SpecFlow.Table(new string[] {
                        "name",
                        "value"});
            table12.AddRow(new string[] {
                        "Routable.Title",
                        "My Blog"});
#line 135
        testRunner.And("I fill in", ((string)(null)), table12);
#line 138
        testRunner.And("I hit \"Save\"");
#line 139
        testRunner.And("I go to \"my-blog\"");
#line 140
    testRunner.Then("I should see \"<link href=\"[^\"]*/XmlRpc/LiveWriter/Manifest\" rel=\"wlwmanifest\" typ" +
                    "e=\"application/wlwmanifest\\+xml\" />\"");
#line 141
    testRunner.When("I go to \"/XmlRpc/LiveWriter/Manifest\"");
#line 142
    testRunner.Then("the content type should be \"\\btext/xml\\b\"");
#line 143
        testRunner.And("I should see \"<manifest xmlns=\"http\\://schemas\\.microsoft\\.com/wlw/manifest/weblo" +
                    "g\">\"");
#line 144
        testRunner.And("I should see \"<clientType>Metaweblog</clientType>\"");
#line hidden
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("The virtual path of my installation when not at the root is reflected in the URL " +
            "example for the slug field when creating a blog or blog post")]
        public virtual void TheVirtualPathOfMyInstallationWhenNotAtTheRootIsReflectedInTheURLExampleForTheSlugFieldWhenCreatingABlogOrBlogPost()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("The virtual path of my installation when not at the root is reflected in the URL " +
                    "example for the slug field when creating a blog or blog post", ((string[])(null)));
#line 146
this.ScenarioSetup(scenarioInfo);
#line 147
    testRunner.Given("I have installed Orchard at \"/OrchardLocal\"");
#line 148
    testRunner.When("I go to \"admin/blogs/create\"");
#line 149
    testRunner.Then("I should see \"<span>http\\://localhost/OrchardLocal/</span>\"");
#line hidden
            TechTalk.SpecFlow.Table table13 = new TechTalk.SpecFlow.Table(new string[] {
                        "name",
                        "value"});
            table13.AddRow(new string[] {
                        "Routable.Title",
                        "My Blog"});
#line 150
    testRunner.When("I fill in", ((string)(null)), table13);
#line 153
        testRunner.And("I hit \"Save\"");
#line 154
        testRunner.And("I go to \"admin/blogs\"");
#line 155
        testRunner.And("I follow \"My Blog\"");
#line 156
        testRunner.And("I follow \"New Post\" where class name has \"primaryAction\"");
#line 157
    testRunner.Then("I should see \"<span>http\\://localhost/OrchardLocal/my-blog/</span>\"");
#line hidden
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("The virtual path of my installation when at the root is reflected in the URL exam" +
            "ple for the slug field when creating a blog or blog post")]
        public virtual void TheVirtualPathOfMyInstallationWhenAtTheRootIsReflectedInTheURLExampleForTheSlugFieldWhenCreatingABlogOrBlogPost()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("The virtual path of my installation when at the root is reflected in the URL exam" +
                    "ple for the slug field when creating a blog or blog post", ((string[])(null)));
#line 159
this.ScenarioSetup(scenarioInfo);
#line 160
    testRunner.Given("I have installed Orchard at \"/\"");
#line 161
    testRunner.When("I go to \"admin/blogs/create\"");
#line 162
    testRunner.Then("I should see \"<span>http\\://localhost/</span>\"");
#line hidden
            TechTalk.SpecFlow.Table table14 = new TechTalk.SpecFlow.Table(new string[] {
                        "name",
                        "value"});
            table14.AddRow(new string[] {
                        "Routable.Title",
                        "My Blog"});
#line 163
    testRunner.When("I fill in", ((string)(null)), table14);
#line 166
        testRunner.And("I hit \"Save\"");
#line 167
        testRunner.And("I go to \"admin/blogs\"");
#line 168
        testRunner.And("I follow \"My Blog\"");
#line 169
        testRunner.And("I follow \"New Post\" where class name has \"primaryAction\"");
#line 170
    testRunner.Then("I should see \"<span>http\\://localhost/my-blog/</span>\"");
#line hidden
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("I set my blog to be the content for the home page and the posts for the blog be r" +
            "ooted to the app")]
        public virtual void ISetMyBlogToBeTheContentForTheHomePageAndThePostsForTheBlogBeRootedToTheApp()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("I set my blog to be the content for the home page and the posts for the blog be r" +
                    "ooted to the app", ((string[])(null)));
#line 172
this.ScenarioSetup(scenarioInfo);
#line 173
    testRunner.Given("I have installed Orchard");
#line 174
    testRunner.When("I go to \"admin/blogs/create\"");
#line hidden
            TechTalk.SpecFlow.Table table15 = new TechTalk.SpecFlow.Table(new string[] {
                        "name",
                        "value"});
            table15.AddRow(new string[] {
                        "Routable.Title",
                        "My Blog"});
            table15.AddRow(new string[] {
                        "Routable.PromoteToHomePage",
                        "true"});
#line 175
        testRunner.And("I fill in", ((string)(null)), table15);
#line 179
        testRunner.And("I hit \"Save\"");
#line 180
        testRunner.And("I go to \"admin/blogs\"");
#line 181
        testRunner.And("I follow \"My Blog\"");
#line 182
        testRunner.And("I follow \"New Post\" where class name has \"primaryAction\"");
#line hidden
            TechTalk.SpecFlow.Table table16 = new TechTalk.SpecFlow.Table(new string[] {
                        "name",
                        "value"});
            table16.AddRow(new string[] {
                        "Routable.Title",
                        "My Post"});
            table16.AddRow(new string[] {
                        "Body.Text",
                        "Hi there."});
#line 183
        testRunner.And("I fill in", ((string)(null)), table16);
#line 187
        testRunner.And("I hit \"Publish Now\"");
#line 188
        testRunner.And("I am redirected");
#line 189
        testRunner.And("I go to \"/\"");
#line 190
    testRunner.Then("I should see \"<h1>My Blog</h1>\"");
#line 191
    testRunner.When("I go to \"/my-blog\"");
#line 192
    testRunner.Then("the status should be 404 \"Not Found\"");
#line 193
    testRunner.When("I go to \"/my-post\"");
#line 194
    testRunner.Then("I should see \"<h1>My Post</h1>\"");
#line hidden
            testRunner.CollectScenarioErrors();
        }
    }
}
#endregion
