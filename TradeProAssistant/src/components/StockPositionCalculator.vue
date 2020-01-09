<template>
    <div class="row row-cols-2">
        <div class="col mb-4">
            <div class="card">
                <div class="card-header">
                    Account
                </div>
                <div class="card-body">
                    <div class="form-group row">
                        <label for="accountBalanceInput" class="col-4 col-form-label">Account Balance</label>
                        <div class="input-group mb-2 col-8">
                            <div class="input-group-prepend">
                                <div class="input-group-text">$</div>
                            </div>
                            <input type="text" class="form-control" id="accountBalanceInput" v-model="accountBalance" />
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="maxRiskPercentageInput" class="col-4 col-form-label">Max Risk %</label>
                        <div class="input-group mb-2 col-8">
                            <input type="text" class="form-control" id="maxRiskPercentageInput" v-model="maxRiskPercentage" />
                            <div class="input-group-append">
                                <div class="input-group-text">%</div>
                            </div>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="maxRiskInput" class="col-4 col-form-label">Max Risk</label>
                        <div class="col-8">
                            <input type="text" readonly class="form-control-plaintext" id="maxRiskInput" :value="formatMoney(maxRisk)" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col mb-4">
            <div class="card">
                <div class="card-header">
                    Position
                </div>
                <div class="card-body">
                    <div class="form-group row">
                        <label for="enterPriceInput" class="col-4 col-form-label">Enter Price</label>
                        <div class="input-group mb-2 col-8">
                            <div class="input-group-prepend">
                                <div class="input-group-text">$</div>
                            </div>
                            <input type="text" class="form-control" id="enterPriceInput" v-model="enterPrice" />
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="stopLossInput" class="col-4 col-form-label">Stop Loss</label>
                        <div class="input-group mb-2 col-8">
                            <div class="input-group-prepend">
                                <div class="input-group-text">$</div>
                            </div>
                            <input type="text" class="form-control" id="stopLossInput" v-model="stopLoss" />
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="riskPerShareInput" class="col-4 col-form-label">Risk/Share</label>
                        <div class="input-group col-8">
                            <input type="text" readonly class="form-control-plaintext" id="riskPerShareInput" :value="formatMoney(riskPerShare)" />
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="sharesInput" class="col-4 col-form-label">Shares</label>
                        <div class="input-group col-8">
                            <input type="text" readonly class="form-control-plaintext" id="sharesInput" :value="shares" />
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="costInput" class="col-4 col-form-label">Cost</label>
                        <div class="input-group col-8">
                            <input type="text" readonly class="form-control-plaintext" id="costInput" :value="formatMoney(cost)" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import formatMoney from "accounting-js/lib/formatMoney";

export default {
        name: 'StockPositionCalculator',
        data: function () {
            return {
                accountBalance: 16000,
                maxRiskPercentage: 2,
                enterPrice: 100,
                stopLoss: 95
            };
        },
        methods: {
            formatMoney: formatMoney
        },
        computed: {
            maxRisk() {
                return this.accountBalance * this.maxRiskPercentage / 100;
            },
            riskPerShare() {
                return Math.abs(this.enterPrice - this.stopLoss);
            },
            shares() {
                return Math.floor(this.maxRisk / this.riskPerShare);
            },
            cost() {
                return (Math.floor(this.maxRisk / this.riskPerShare)) * this.enterPrice;
            }
        }
}
</script>
