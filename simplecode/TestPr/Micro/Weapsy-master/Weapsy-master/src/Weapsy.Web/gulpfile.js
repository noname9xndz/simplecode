﻿/// <binding AfterBuild='copy-apps' Clean='clean' />
"use strict";

var gulp = require("gulp"),
    rimraf = require("rimraf"),
    concat = require("gulp-concat"),
    cssmin = require("gulp-cssmin"),
    uglify = require("gulp-uglify"),
    del = require('del'),
    vinylPaths = require('vinyl-paths');

var webroot = "./wwwroot/";

var paths = {
    js: webroot + "js/**/*.js",
    minJs: webroot + "js/**/*.min.js",
    css: webroot + "css/**/*.css",
    minCss: webroot + "css/**/*.min.css",
    concatJsDest: webroot + "js/site.min.js",
    concatCssDest: webroot + "css/site.min.css",
    devApps: "../",
    clientApps: "./Apps/"
};

var apps = [
    'Weapsy.Apps.Text'
];

gulp.task('clean-apps', function () {
    var appsToDelete = [];
    apps.forEach(function (app) {
        appsToDelete.push(paths.clientApps + app);
    });
    return del(appsToDelete);
});

gulp.task('copy-apps', ['clean-apps'], function () {
    apps.forEach(function (app) {
        gulp.src(paths.devApps + app + '/wwwroot/**/*.*')
            .pipe(gulp.dest(paths.clientApps + app + '/wwwroot/'));
        gulp.src(paths.devApps + app + '/Views/**/*.*')
            .pipe(gulp.dest(paths.clientApps + app + '/Views/'));
        gulp.src(paths.devApps + app + '/bin/Debug/netcoreapp2.0/**/*.*')
            .pipe(gulp.dest(paths.clientApps + app));
    });
});

gulp.task("clean:js", function (cb) {
    rimraf(paths.concatJsDest, cb);
});

gulp.task("clean:css", function (cb) {
    rimraf(paths.concatCssDest, cb);
});

gulp.task("clean", ["clean:js", "clean:css"]);

gulp.task("min:js", function () {
    return gulp.src([paths.js, "!" + paths.minJs], { base: "." })
        .pipe(concat(paths.concatJsDest))
        .pipe(uglify())
        .pipe(gulp.dest("."));
});

gulp.task("min:css", function () {
    return gulp.src([paths.css, "!" + paths.minCss])
        .pipe(concat(paths.concatCssDest))
        .pipe(cssmin())
        .pipe(gulp.dest("."));
});

gulp.task("min", ["min:js", "min:css"]);
