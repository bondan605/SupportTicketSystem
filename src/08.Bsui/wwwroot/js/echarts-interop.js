window.echartsInterop = {
    charts: {},

    initChart: function (elementId, optionJson) {
        const el = document.getElementById(elementId);
        if (!el) {
            console.warn(`echartsInterop: element '${elementId}' not found.`);
            return;
        }

        this.disposeChart(elementId);

        this.whenSized(el, () => {
            const chart = echarts.init(el);
            const finalOption = JSON.parse(optionJson);
            chart.setOption(this.zeroOutOption(finalOption));

            requestAnimationFrame(() => {
                if (this.charts[elementId] !== chart) {
                    return;
                }

                chart.setOption(finalOption, true);
            });

            this.charts[elementId] = chart;

            const resizeObserver = new ResizeObserver(() => chart.resize());
            resizeObserver.observe(el);
            chart.__resizeObserver = resizeObserver;
        });
    },

    updateChart: function (elementId, optionJson) {
        const chart = this.charts[elementId];
        if (!chart) {
            this.initChart(elementId, optionJson);
            return;
        }

        chart.setOption(JSON.parse(optionJson), true);
    },

    disposeChart: function (elementId) {
        const chart = this.charts[elementId];
        if (chart) {
            if (chart.__resizeObserver) {
                chart.__resizeObserver.disconnect();
            }
            chart.dispose();
            delete this.charts[elementId];
        }
    },

    whenSized: function (el, callback, maxAttempts) {
        maxAttempts = maxAttempts || 20;
        let lastWidth = -1;
        let lastHeight = -1;
        let attempts = 0;

        const check = () => {
            if (!document.body.contains(el)) {
                return;
            }

            const width = el.clientWidth;
            const height = el.clientHeight;
            const isStable = width > 0 && height > 0 && width === lastWidth && height === lastHeight;

            attempts++;

            if (isStable || attempts >= maxAttempts) {
                callback();
                return;
            }

            lastWidth = width;
            lastHeight = height;
            requestAnimationFrame(check);
        };

        requestAnimationFrame(check);
    },

    zeroOutOption: function (option) {
        const clone = JSON.parse(JSON.stringify(option));

        if (Array.isArray(clone.series)) {
            clone.series.forEach(series => {
                if (Array.isArray(series.data)) {
                    series.data = series.data.map(point => {
                        if (point !== null && typeof point === 'object') {
                            return { ...point, value: 0 };
                        }

                        return 0;
                    });
                }
            });
        }

        return clone;
    }
};
