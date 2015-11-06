using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SPMeta2.Definitions;
using SPMeta2.Enumerations;
using SPMeta2.Syntax.Default;
using Microsoft.SharePoint.Client;
using SPMeta2.CSOM.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Publishing.Navigation;
using Microsoft.SharePoint.Client.Taxonomy;
using SPMeta2.CSOM.Services;
using SPMeta2.Enumerations;
using SPMeta2.Models;
using SPMeta2.Standard.Definitions;
using SPMeta2.Standard.Definitions.Fields;
using SPMeta2.Standard.Syntax;
using SPMeta2.Standard.Definitions.Taxonomy;
using SPMeta2.Syntax.Default;
using SPMeta2.Syntax.Default.Modern;
using SPMeta2.Utils;


namespace SPMeta2ManualTest
{
    public static class SimpleModel
    {

        public static void Provision(ClientContext context, CSOMProvisionService provisioningService)
        {

            context.Site.EnsureProperties("ServerRelativeUrl");

            // model for Site Collection artifacts for stories
            var rootSiteModel = SPMeta2Model.NewSiteModel(site =>
            {
                site.AddTaxonomyTermStore(
                    new TaxonomyTermStoreDefinition() { UseDefaultSiteCollectionTermStore = true},
                    store =>
                    {
                        store.AddTaxonomyTermGroup(new TaxonomyTermGroupDefinition() {IsSiteCollectionGroup = true},
                            group =>
                            {
                                group.AddTaxonomyTermSet(new TaxonomyTermSetDefinition()
                                {
                                    Name = "SPMETA2Test",
                                    LCID = 1033
                                },
                                    termset =>
                                    {
                                        termset.AddTaxonomyTerm(new TaxonomyTermDefinition() {Name = "testterm", LCID = 1033});
                                    });
                            });
                    }
                    );


            });
            var rootSiteModel2 = SPMeta2Model.NewSiteModel(site =>
            {

                site.AddTaxonomyField(new TaxonomyFieldDefinition()
                {
                    InternalName = "sptaxfieldtest1",
                    Group = "spmeta2test",
                    Title = "TextTaxonomyField",
                    IsSiteCollectionGroup = true,
                    //TermGroupName = "Site Collection - mbakirov367.sharepoint.com",
                    UseDefaultSiteCollectionTermStore = true,
                    TermSetName = "SPMETA2Test",
                    TermSetLCID = 1033,
                    TermName = "testterm",
                    TermLCID = 1033
                }
                    );
            });


            // model for Site Collection artifacts for stories
            var rootWebModel = SPMeta2Model.NewWebModel(web =>
            {
 
            });

            provisioningService.DeploySiteModel(context, rootSiteModel);
            Thread.Sleep(1000);
            provisioningService.DeploySiteModel(context, rootSiteModel2);
            provisioningService.DeployWebModel(context, rootWebModel);

        }



    }
    }
