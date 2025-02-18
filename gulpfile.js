'use strict';

const gulp = require('gulp');
const yargs = require('yargs');
const chalk = require('chalk');
const { spawn } = require('child_process');
const { join } = require('path');
const openBrowser = require('react-dev-utils/openBrowser');

const { log } = console;

let spawnOptions = {
  stdio: 'inherit',  // Agora estamos redirecionando a saída para o terminal
  detached: true,
  shell: true
};

function logInfo(...message) {
  log(chalk.bgCyanBright(message.join(' ')));
}

function spawnProcess(command, params = [], options) {
  const process = spawn(command, params, {
    ...spawnOptions,
    ...options,
    stdio: 'inherit' // Redirecionando a saída para o terminal
  });

  process.on('close', (code) => {
    if (code !== 0) {
      logInfo(`Processo ${command} finalizou com código: ${code}`);
    }
  });

  return process;
}

const args = yargs(process.argv.slice(2))
  .option('build-backend', {
    alias: 'backend',
    type: 'boolean',
    description: 'Fazer uma nova build do backend ou usar a última realizada',
    default: true
  })
  .option('run-front', {
    alias: 'frontend',
    type: 'boolean',
    description: 'Executar o projeto de front-end.',
    default: true
  })
  .help()
  .argv;

const projectsPath = process.cwd();
const backendPath = join(projectsPath, 'CdbCalculator.API');
const frontendPath = join(projectsPath, 'CdbCalculator.Frontend');

gulp.task('build_backend', done => {
  if (args.buildBackend) {
    logInfo('Building backend...');
    spawnProcess('dotnet', ['build'], { cwd: backendPath }).on('close', done);
  } else {
    logInfo('Skipping backend build...');
    done();
  }
});

gulp.task('run_backend', done => {
  logInfo('Running backend...');
  const backendProcess = spawnProcess('dotnet', ['run'], { cwd: backendPath });

  backendProcess.on('close', done);
});

gulp.task('run_frontend', done => {
  if (args.runFront) {
    logInfo('Running frontend...');
    const child = spawnProcess('ng', ['serve'], { cwd: frontendPath });

    if (!child) {
      logInfo('Erro ao tentar iniciar o processo do frontend.');
      done();
      return;
    }

    // Aguardar 5 segundos antes de abrir o navegador
    setTimeout(() => {
      logInfo('Frontend is running, opening browser...');
      openBrowser('http://localhost:4200');
    }, 5000);  // 5 segundos de espera

    child.on('close', done);
  } else {
    logInfo('Skipping frontend run...');
    done();
  }
});

gulp.task('open_browser', done => {
  logInfo('Opening browser...');
  openBrowser('http://localhost:4200'); // Supondo que o Angular esteja rodando na porta padrão 4200
  done();
});

gulp.task('run', gulp.series('build_backend', gulp.parallel('run_backend', 'run_frontend'), 'open_browser'));

gulp.task('default', gulp.series('run'));