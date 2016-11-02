var gulp = require("gulp");
var bower = require("main-bower-files");
var $ = require("gulp-load-plugins")({
    rename: {
        'gulp-bower-normalize': 'bowerNormalize'
    }
});

// Configurations
var config = {
    // NPM & Bower path
    npm: {
        file: './package.json'
    },
    bower: {
        file: './bower.json',
        lib: './bower_components/'
    },

    // Export
    exportPath: './App_Plugins/LeReport/lib/'
}


// Tasks

gulp.task('bower', function () {
    // export js/css vendors specified in bower.json

    return gulp.src(bower(), { base: config.bower.lib })
        .pipe($.bowerNormalize({ bowerJson: config.bower.file }))
        .pipe(gulp.dest(config.exportPath));
});

gulp.task('default', []);