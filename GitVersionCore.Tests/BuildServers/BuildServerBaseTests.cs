﻿using System;
using System.Collections.Generic;
using GitVersion;
using NUnit.Framework;
using Shouldly;

[TestFixture]
public class BuildServerBaseTests 
{
    [Test]
    public void BuildNumberIsFullSemVer()
    {
        var writes = new List<string>();
        var semanticVersion = new SemanticVersion
        {
            Major = 1,
            Minor = 2,
            Patch = 3,
            PreReleaseTag = "beta1",
            BuildMetaData = "5"
        };

        semanticVersion.BuildMetaData.CommitDate = DateTimeOffset.Parse("2014-03-06 23:59:59Z");
        semanticVersion.BuildMetaData.Sha = "commitSha";
        var variables = VariableProvider.GetVariablesFor(semanticVersion, AssemblyVersioningScheme.MajorMinorPatch, VersioningMode.ContinuousDelivery, "ci", false);
        new BuildServer().WriteIntegration(writes.Add, variables);

        writes[1].ShouldBe("1.2.3-beta.1+5");
    }

    class BuildServer : BuildServerBase
    {
        public override bool CanApplyToCurrentContext()
        {
            throw new NotImplementedException();
        }

        public override string GenerateSetVersionMessage(string versionToUseForBuildNumber)
        {
            return versionToUseForBuildNumber;
        }

        public override string[] GenerateSetParameterMessage(string name, string value)
        {
            return new string[0];
        }
    }
}