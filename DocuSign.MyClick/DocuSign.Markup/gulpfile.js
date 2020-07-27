const gulp        = require('gulp'),
    browserSync   = require('browser-sync').create(),
    autoprefixer  = require('gulp-autoprefixer'),
    sass          = require('gulp-sass'),
    cleanCSS      = require('gulp-clean-css'),
    rename        = require("gulp-rename"),
    newer         = require("gulp-newer"),
    plumber       = require('gulp-plumber'),
    strip         = require('gulp-strip-comments'),
    nunjucks      = require('gulp-nunjucks'),
    groupqueries  = require('gulp-group-css-media-queries'),
    watch         = require('gulp-watch'),
    webp          = require('gulp-webp'),
    clean         = require("gulp-clean");

const paths = {
    templates: {
        src: 'src/templates/*.html',
        dest: 'build',
        watch: 'src/templates/**/*.html',
    },
    styles: {
        src: 'src/assets/scss/main.scss',
        dest: 'build/assets/css',
        watch: 'src/assets/scss/**/*.scss'
    },
    images: {
        src: 'src/assets/img/*',
        dest: 'build/assets/img'
    }
};

gulp.task("templates", function() {
    return gulp.src(paths.templates.src)
        .pipe(nunjucks.compile({}, {
            autoescape: true,
            watch: true
        }))
        .pipe((strip()))
        .pipe(gulp.dest(paths.templates.dest))
        .on("end", browserSync.reload);
});

gulp.task("styles", function() {
    return gulp.src(paths.styles.src)
        .pipe(plumber())
        .pipe(sass())
        .pipe(groupqueries())
        .pipe(autoprefixer())
        .pipe(cleanCSS())
        .pipe(rename({suffix: ".min"}))
        .pipe(gulp.dest(paths.styles.dest))
        .on("end", browserSync.reload);
});

gulp.task("images", function() {
    return gulp.src(paths.images.src)
        .pipe(newer(paths.images.dest))
        .pipe(gulp.dest(paths.images.dest))
        .pipe(webp({quality: 90}))
        .pipe(gulp.dest(paths.images.dest))
        .on("end", browserSync.reload);
});

gulp.task("watch", function() {
    return new Promise((res, rej) => {
        watch(paths.templates.watch, gulp.series("templates"));
        watch(paths.styles.watch, gulp.series("styles"));
        watch(paths.images.src, gulp.series("images"));
        res();
    });
});

gulp.task("clean", function() {
    return gulp.src("./build/*", {read: false})
        .pipe(clean())
});

gulp.task("serve", function () {
    return new Promise((res, rej) => {
        browserSync.init({
            server: "./build/",
            port: 4201
        });
        res();
    });
});


/*
* Default task
* */
gulp.task("default", gulp.series("clean",
    gulp.parallel("templates", "styles", "images"),
    gulp.parallel("watch", "serve")
));
