# 構成想定

このプログラムの構成の案。テストアプリを書いてて、割と行き詰ったので。

## 完成像

### .NETテストアプリでの要件

- 対人オンリー
 - とりあえずAIは考えずに手入力のIOを考える
 - プロトコルもなし
- CUI版、GUI版作成
- C#簡易アプリ→設計した後F# Manager & C# GUI

### Webアプリ化（Typescript？）

- GUIをブラウザ（HTML/CSS/Javascript）へ移行
- プログラム全体をJavascript移植
 - サーバー：node.js（WebSocket？）
 - クライアント:ブラウザ（ReactJS + CreateJS）
- プロトコルの策定
 - USI ext + IRC
- プロトコル対応
 - ルーム関連への対応
 - AI動作対応
- AI開発者向けクライアント作成
 - USI to JSON or DSL or PlainInput
 - 簡易ボット作成対応

### Webサービス化

- ユーザー管理機能
 - ログイン、ログアウト、入会、退会
 - レート機能
 - AIプログラムのハブ管理化（GitHub提携化？）

## ゲームの流れ

<pre>
game.newgame();
game.start();
while(game.isend()){
    var p = game.getnextplayer();
    var s = game.getnowstatus();
    game.nextaction(p.getaction(s));
}
game.end();
</pre>

## 構造案

### on C# 

#### namespaces

- TestApp
 - メイン空間
 - Common const values、Common static function
 - Main関数
- TestApp.Board
 - 駒・盤面の情報・状態を表すクラス・構造体・定数
- TestApp.Manager
 - ゲームの進行を補うインターフェース・クラス
- TestApp.Client
 - プレイヤー情報・IOを司るインターフェース・クラス
- TestApp.UI
 - CUI・GUIのインターフェース・クラス

#### const values
using TestApp;

- Board.KomaType(enum)
 - 駒の種類
- Board.PlayerType(enum)
 - プレイヤーの種類
- Board.BoardType(enum)
 - ゲームの種類

#### Interfaces and abstract classes
using TestApp;

- Board.BaseBoard
 - ボードの基盤
- UI.IIOManager
 - UIのマネージャー基盤
- Client.IPlayer
 - クライアントの基盤

#### classes
using TestApp;


## ToDo

## 簡易メモ

## 問題点