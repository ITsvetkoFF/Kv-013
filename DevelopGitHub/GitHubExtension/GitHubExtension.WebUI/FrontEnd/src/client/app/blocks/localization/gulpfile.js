var gulp = require('gulp');
var ngConstant = require('gulp-ng-constant');

gulp.task('constants', function () {
    var myConfig = require('./config.json');
    var envConfig = myConfig[process.env];
    return ngConstant({
        constants: envConfig,
        stream: true
    })
      .pipe(gulp.dest('dist'));
});