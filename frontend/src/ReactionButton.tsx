import React, {Component} from 'react';
import './ReactionButton.css';

enum ReactionState {
    OFF,
    RED,
    GREEN,
};

function delay(ms: number) {
    return new Promise(resolve => setTimeout(resolve, ms));
}

class ReactionButton extends Component {
    state = {
        latestStartTime: 0,
        reactionTime: null,
        greenLightTime: 0,

        tries: 0,
        reactionTimeSum: 0,

        label: "TEST REACTION TIME",
        class: "whiteReactionBtn",
        reactionState: ReactionState.OFF,
    };

    clickOnOff = async() => {
        const tmp = await fetch("http://localhost:5252/reactiontime");
        const json = await tmp.json();

        this.setState({
            label: "WAIT",
            class: "redReactionBtn",
            reactionState: ReactionState.RED,
        });

        const startTime = performance.now();
        this.setState({latestStartTime: startTime});
        
        await delay(json.millisecondsToWait);

        if (
            this.state.reactionState === ReactionState.RED 
         && this.state.latestStartTime === startTime
        ) {
            this.setState({
                label: "PRESS NOW",
                class: "greenReactionBtn",
                reactionState: ReactionState.GREEN,
            });
        }
        this.setState({greenLightTime: performance.now()});
    };
    clickOnRed = () => {
        this.setState({
            label: "TOO SOON, TRY AGAIN",
            class: "orangeReactionBtn",
            reactionState: ReactionState.OFF,
        });
    }
    clickOnGreen = (timeNow: number) => {
        const renderingDelay = 75;
        const currentReactionTime = timeNow - this.state.greenLightTime - renderingDelay;
        this.setState({
            reactionTime: currentReactionTime,
            
            tries: this.state.tries + 1,
            reactionTimeSum: this.state.reactionTimeSum + currentReactionTime,

            label: "TRY AGAIN",
            class: "whiteReactionBtn",
            reactionState: ReactionState.OFF,
        });
    }

    handleClick = async() => {
        const timeNow = performance.now();
        switch (this.state.reactionState) {
        case ReactionState.OFF:
            this.clickOnOff();
            break;
        case ReactionState.RED:
            this.clickOnRed();
            break;
        case ReactionState.GREEN:
            this.clickOnGreen(timeNow);
            break;
        default:
        }
    };

    render() {
        const mainLabel = this.state.reactionTime != null 
                ? "Reaction time: " + this.state.reactionTime + " ms" 
                : "TEST YOUR REACTION TIME";

        let averageLabel = "";
        if (this.state.tries > 0) {
            averageLabel += "Average reaction time: " + Math.floor(this.state.reactionTimeSum / this.state.tries) + " ms";
            averageLabel += " ";
            averageLabel += "(" + this.state.tries + " " + (this.state.tries === 1 ? "try" : "tries") + ")";
        }
        return (
            <>
            <h1>{mainLabel}</h1>
            <button 
              id="reactionBtn"
              className={this.state.class}
              onClick={this.handleClick}
            >
                {this.state.label}
            </button>
            <p>{averageLabel}</p>
            </>
        )
    }
}

export default ReactionButton;
