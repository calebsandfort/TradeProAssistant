<template>
    <div>
        <div class="form-group row">
            <label for="slots" class="col-sm-2 col-form-label">Slots</label>
            <div class="col-sm-10">
                <input type="text" class="form-control" id="slots" name="slots" placeholder="Slots"
                       :value="settings.slots" @input="debounceInput">
            </div>
        </div>
        <div class="form-group row">
            <label for="minStrikeDiff" class="col-sm-2 col-form-label">Min Strike Diff</label>
            <div class="col-sm-10">
                <input type="text" class="form-control" id="minStrikeDiff" name="minStrikeDiff" placeholder="Min Strike Diff"
                       :value="settings.minStrikeDiff" @input="debounceInput">
            </div>
        </div>
        <div class="form-group row">
            <label for="maxRisk" class="col-sm-2 col-form-label">Max Risk/Pair</label>
            <div class="input-group col-sm-10">
                <div class="input-group-prepend">
                    <div class="input-group-text">$</div>
                </div>
                <input type="text" class="form-control" id="maxRisk" name="maxRisk" placeholder="Max Risk"
                       :value="settings.maxRisk" @input="debounceInput">
            </div>
        </div>
        <div class="form-group row">
            <label for="strikePadding" class="col-sm-2 col-form-label">Strike Padding</label>
            <div class="col-sm-10">
                <input type="text" class="form-control" id="strikePadding" name="strikePadding" placeholder="Strike Padding" 
                       :value="settings.strikePadding" @input="debounceInput">
            </div>
        </div>
        <div class="form-group row">
            <label for="capitalRequirementsInput" class="col-2 col-form-label">Capital Requirements</label>
            <div class="col-10">
                <input id="capitalRequirementsInput" type="text" readonly class="form-control-plaintext" :value="formatMoney(capitalRequirements)" />
            </div>
        </div>
    </div>
</template>

<script>
    import formatMoney from "accounting-js/lib/formatMoney";
    import formatNumber from "accounting-js/lib/formatNumber";
    import { mapState, mapActions, mapGetters } from "vuex";
    import _ from 'lodash';

export default {
        name: 'WeeklyIncomeSettings',
        data: function () {
            return {};
        },
        methods: {
            formatMoney: formatMoney,
            formatNumber: formatNumber,
            ...mapActions({
                setField: "weeklyIncome/setField"
            }),
            fieldChangedNumber: function (name, v) {
                this.setField({
                    name: name,
                    v: parseFloat(v)
                });
            },
            debounceInput: _.debounce(function (e) {
                this.setField({
                    name: `settings.${e.target.name}`,
                    v: parseFloat(e.target.value)
                });
            }, 500)
        },
        computed: {
            ...mapState({
                settings: state => state.weeklyIncome.settings
            }),
            ...mapGetters({
                capitalRequirements: "weeklyIncome/capitalRequirements"
            })
        }
}
</script>
