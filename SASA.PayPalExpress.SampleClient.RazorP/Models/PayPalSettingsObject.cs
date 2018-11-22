using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PaymentGateways.PayPal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SASA.PayPalExpress.SampleClient.RazorP.Models
{
    public class PayPalSettingsObject : PayPalSettingsBaseClass
    {
        private readonly string liveClientId;
        private readonly string sandBoxClientId;
        private readonly string liveKey;
        private readonly string sandBoxKey;

        public PayPalSettingsObject(IRepositoryPayPalBearersToken repositoryPayPalBearersToken, IConfiguration configuration) : base(repositoryPayPalBearersToken)
        {
            liveClientId = configuration.GetSection("PayPal").GetSection("PayPalLiveCliendId").Value;
            sandBoxClientId = configuration.GetSection("PayPal").GetSection("PayPalSandBoxCliendId").Value;
            liveKey = configuration.GetSection("PayPal").GetSection("PayPalLiveKey").Value;
            sandBoxKey = configuration.GetSection("PayPal").GetSection("PayPalSandBoxKey").Value;
        }
        

        public override string LiveClientId()
        {
            return liveClientId;
        }

        public override string LiveKey()
        {
            return liveKey;
        }

        public override PayPalMode PayPalMode()
        {
            return PaymentGateways.PayPal.PayPalMode.Sandbox;
        }

        public override string SandboxClientId()
        {
            return sandBoxClientId;
        }

        public override string SandboxKey()
        {
            return sandBoxKey;
        }
    }
}
