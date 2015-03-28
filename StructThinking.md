# 構成想定

このプログラムの構成の案。テストアプリを書いてて、割と行き詰ったので。

## 完成像

### .NETテストアプリでの要件

- 対人オンリー
 - とりあえずAIは考えずに手入力のIOを考える
 - プロトコルもなし
- 通常の将棋モードだけ実装
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
- ログ・ゲーム記録機能整備
- AI開発者向けクライアント作成
 - USI to JSON or DSL or PlainInput
 - 簡易ボット作成対応
- 他の類似将棋ゲームにも対応
 - せめて5五将棋には対応：電通大の調布祭AIコンテストがこれなので

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

### on CSharp

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
 - NONE, OUSHO, GYOKU, HISHA, RYUOU, KAKUGYO, RYUMA, KINSHO,
 - GINSHO, NARIGIN, KEIMA, NARIKEI, KYOSHA, NARIKYO, FUHYO,
- Board.PlayerType(enum)
 - プレイヤーの種類
 - NONE, SENTE, GOTE, 
- Board.BoardType(enum)
 - ゲームの種類
 - Normal, Mini, Yonin, Tsume, Hasami,

#### Interfaces and abstract classes
using TestApp;

- Board.BaseBoard
 - ボードの基盤
 - komaStates (abstract property) -- get-onlyプロパティ。駒の状態を保持
 - boardWidth (property) -- get-onlyプロパティ。ボードの横の長さを保持
 - boardHeight (property) -- get-onlyプロパティ。ボードの縦の長さを保持
 - GetNowBoardStatus (method) -- 現在のボードの状態をBoardStateで返す
 - GetPlayers (abstract method) -- 全てのプレイヤーを返す。
- UI.IIOManager
 - UIのマネージャー基盤
 - AddClient (method) -- クライアントを追加（IPlayer）演算子+=割り当て？
 - RemoveClient (method) -- クライアントを削除（IPlayer) 演算子-=割り当て？
- Manager.IGameManager
 - AddClient (method) -- クライアントを追加（IPlayer）演算子+=割り当て？
 - RemoveClient (method) -- クライアントを削除（IPlayer) 演算子-=割り当て？
- Client.IPlayer
 - クライアントの基盤

#### classes and structs
using TestApp;

- Board.NormalBoard : Board.BaseBoard
 - 通常の将棋のボードクラス
- Board.KomaState
 - 駒の状態を表す構造体
- Board.KomaPoint
 - 駒の位置を表す構造体
 - r = row (member) -- 行番号
 - c = col = column (member) -- 列番号
- Board.BoardState
 - ボードの状態を表すクラス
- Board.BoardAction
 - ボードの状態の操作を表す構造体
- UI.GUIManager : UI.IIOManager
 - GUIのマネージャークラス
- UI.GUIForm
 - GUIのフォームクラス
- UI.CUIManager : UI.IIOManager
 - CUIのマネージャークラス
- Client.AutoPlayer : Client.IPlayer
 - テスト用の自動打ちプレーヤークラス
- Client.ManPlayer : Client.IPlayer
 - IIOManagerからアクションをもらって打つプレーヤークラス
- Manager.NormalGameManager : Manager.IGameManager
 - 通常の将棋ゲームの進行を管理するクラス

## 簡易メモ

## 問題点


