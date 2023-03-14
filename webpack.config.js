const path = require('path');
module.exports = {
    mode: 'development',
    entry: './wwwroot/index.html',
    output: {
        filename: 'bundle.js',
        path: path.resolve(__dirname, 'wwwroot/js')
    },
    module: {
        rules: [
            {
                test: /\.css$/i,
                use: ['style-loader', 'css-loader'],
            },
            {
                test: /\.html$/i,
                loader: 'html-loader',
            },
            {
                test: /\.js$/,
                exclude: /node_modules/,
                use: {
                    loader: 'babel-loader',
                    options: {
                        presets: ['@babel/preset-env']
                    }
                }
            }
        ]
    }
};



