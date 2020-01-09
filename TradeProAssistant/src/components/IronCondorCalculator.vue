<template>
    <div class="row row-cols-2">
        <div class="col mb-4">
            <div class="card">
                <div class="card-header">
                    Details
                </div>
                <div class="card-body">
                    <div class="form-group row">
                        <label for="strikeDifferenceInput" class="col-4 col-form-label">Strike Difference</label>
                        <div class="input-group mb-2 col-8">
                            <div class="input-group-prepend">
                                <div class="input-group-text">$</div>
                            </div>
                            <input type="text" class="form-control" id="strikeDifferenceInput" v-model="strikeDifference" />
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="netCreditInput" class="col-4 col-form-label">Net Credit</label>
                        <div class="input-group mb-2 col-8">
                            <div class="input-group-prepend">
                                <div class="input-group-text">$</div>
                            </div>
                            <input type="text" class="form-control" id="netCreditInput" v-model="netCredit" />
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="maxRiskInput" class="col-4 col-form-label">Max Risk</label>
                        <div class="col-8">
                            <input type="text" readonly class="form-control-plaintext" id="maxRiskInput" :value="formatMoney(maxRisk)" />
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="maxRewardInput" class="col-4 col-form-label">Max Reward</label>
                        <div class="col-8">
                            <input type="text" readonly class="form-control-plaintext" id="maxRewardInput" :value="formatMoney(maxReward)" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
       <div class="col mb-4">
        <div class="card">
            <div class="card-header">
                Risk Management
            </div>
            <div class="card-body">
                <div class="form-group row">
                    <label for="lowerFailureProbabilityInput" class="col-4 col-form-label">Lower Failure</label>
                    <div class="input-group mb-2 col-8">
                        <input type="text" class="form-control" id="lowerFailureProbabilityInput" v-model="lowerFailureProbability" />
                        <div class="input-group-append">
                            <div class="input-group-text">%</div>
                        </div>
                    </div>
                </div>
                <div class="form-group row">
                    <label for="higherFailureProbabilityInput" class="col-4 col-form-label">Higher Failure</label>
                    <div class="input-group mb-2 col-8">
                        <input type="text" class="form-control" id="higherFailureProbabilityInput" v-model="higherFailureProbability" />
                        <div class="input-group-append">
                            <div class="input-group-text">%</div>
                        </div>
                    </div>
                </div>
                <div class="form-group row">
                    <label for="failureProbabilityInput" class="col-4 col-form-label">Failure Probability</label>
                    <div class="col-8">
                        <input type="text" readonly class="form-control-plaintext" id="failureProbabilityInput" :value="`${formatNumber(failureProbability)}%`" />
                    </div>
                </div>
                <div class="form-group row">
                    <label for="successProbabilityInput" class="col-4 col-form-label">Success Probability</label>
                    <div class="col-8">
                        <input type="text" readonly class="form-control-plaintext" id="successProbabilityInput" :value="`${formatNumber(successProbability)}%`" />
                    </div>
                </div>
                <div class="form-group row">
                    <label for="expectedLossInput" class="col-4 col-form-label">Expected Loss</label>
                    <div class="col-8">
                        <input type="text" readonly class="form-control-plaintext" id="expectedLossInput" :value="formatMoney(expectedLoss)" />
                    </div>
                </div>
                <div class="form-group row">
                    <label for="expectedWinInput" class="col-4 col-form-label">Expected Win</label>
                    <div class="col-8">
                        <input type="text" readonly class="form-control-plaintext" id="expectedWinInput" :value="formatMoney(expectedWin)" />
                    </div>
                </div>
            </div>
        </div>
    </div>
            </div>
</template>

<script>
    import formatMoney from "accounting-js/lib/formatMoney";
    import formatNumber from "accounting-js/lib/formatNumber";

export default {
        name: 'IronCondorCalculator',
        data: function () {
            return {
                strikeDifference: 5,
                netCredit: 2.5,
                lowerFailureProbability: 10,
                higherFailureProbability: 10
            };
        },
        methods: {
            formatMoney: formatMoney,
            formatNumber: formatNumber
        },
        computed: {
            maxRisk() {
                return (this.strikeDifference * 100) - (this.netCredit * 100);
            },
            maxReward() {
                return this.netCredit * 100;
            },
            strikeDifferenceDollars() {
                return this.strikeDifference * 100;
            },
            failureProbability() {
                return parseFloat(this.lowerFailureProbability) + parseFloat(this.higherFailureProbability);
            },
            successProbability() {
                return 100 - (parseFloat(this.lowerFailureProbability) + parseFloat(this.higherFailureProbability));
            },
            expectedLoss() {
                return ((this.strikeDifference * 100) - (this.netCredit * 100)) * ((parseFloat(this.lowerFailureProbability) + parseFloat(this.higherFailureProbability)) / 100);
            },
            expectedWin() {
                return ((100 - (parseFloat(this.lowerFailureProbability) + parseFloat(this.higherFailureProbability))) / 100) * (this.netCredit * 100);
            }
            //shares() {
            //    return Math.floor(this.maxRisk / this.riskPerShare);
            //},
            //cost() {
            //    return (Math.floor(this.maxRisk / this.riskPerShare)) * this.enterPrice;
            //}
        }
}
</script>
