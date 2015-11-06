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
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Publishing.Navigation;
using Microsoft.SharePoint.Client.Taxonomy;
using SPMeta2.CSOM.Services;
using SPMeta2.Enumerations;
using SPMeta2.Models;
using SPMeta2.Standard.Definitions;
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
                                        termset.AddTaxonomyTerm(new TaxonomyTermDefinition() {Name = "testterm"});
                                    });
                            });
                    }
                    );
            });


            // model for Site Collection artifacts for stories
            var rootWebModel = SPMeta2Model.NewWebModel(web =>
            {
 
            });

            provisioningService.DeploySiteModel(context, rootWebModel);
            provisioningService.DeployWebModel(context, rootWebModel);

        }



    }
    }
